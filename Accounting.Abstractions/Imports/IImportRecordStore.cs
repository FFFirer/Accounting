namespace Accounting.Imports;

public interface IImportRecordStore {
    Task SaveAsync(ImportRecord record, CancellationToken cancellationToken);
}