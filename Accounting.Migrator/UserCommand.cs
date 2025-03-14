using System.CommandLine;
using System.CommandLine.Invocation;

using Accounting;
using Accounting.Migrator;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class UserCommand : Command
{
    public UserCommand() : base("user")
    {
        this.AddCommand(new AddUserCommand());
    }

    public class AddUserCommand : Command
    {

        static Option<string> UserNameOption = new Option<string>("--username") { };
        static Option<string> PasswordOption = new Option<string>("--password") { IsRequired = true };
        static Option<string> EmailOption = new Option<string>("--email") { IsRequired = true };

        public AddUserCommand() : base("add")
        {
            this.AddOption(UserNameOption);
            this.AddOption(PasswordOption);
            this.AddOption(EmailOption);

            this.SetHandler(AddUser);
        }

        private async Task AddUser(InvocationContext context)
        {
            var username = context.ParseResult.GetValueForOption(UserNameOption);
            var password = context.ParseResult.GetValueForOption(PasswordOption);
            var email = context.ParseResult.GetValueForOption(EmailOption);

            var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);

            var provider = Helper.BuildServiceProvider(connectionName ?? "Default");

            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userStore = provider.GetRequiredService<IUserStore<User>>();

            var user = new User();

            await userStore.SetUserNameAsync(user, username ?? email, CancellationToken.None);
            var emailStore = GetEmailStore(userManager, userStore);
            emailStore?.SetEmailAsync(user, email, CancellationToken.None);

            var result = await userManager.CreateAsync(user, password!);

            if (result.Succeeded)
            {
                Console.WriteLine("User added!");
                return;
            }

            Console.WriteLine("Errors: ");
            foreach (var error in result.Errors)
            {
                Console.WriteLine("{0}: {1}", error.Code, error.Description);
            }
        }

        private IUserEmailStore<User>? GetEmailStore(UserManager<User> userManager, IUserStore<User> userStore)
        {
            if (userManager.SupportsUserEmail)
            {
                return (IUserEmailStore<User>)userStore;
            }

            return null;
        }
    }

}