# Kendo UI FileManager – Azure Blob Storage

## Description
This sample demonstrates how to plug the [`Azure file system provider`](https://github.com/SyncfusionExamples/azure-aspcore-file-provider) into the Kendo UI for jQuery File Manager into an ASP.NET Core project so users can browse, create, rename, delete, cut, copy, paste, upload, download, and otherwise manage blobs from the `Azure blob storage` just like they would files on a disk. The `BlobFileManagerService` wraps Azure Storage SDK types and exposes async operations to keep the File Manager UI responsive while mirroring every change to Azure Blob Storage.

To run the sample against your own data, start by creating an `Azure Storage account` with a blob container (for example, by following the official [`Create a storage account`](https://learn.microsoft.com/azure/storage/common/storage-account-create?tabs=azure-portal) quickstart). Once the account exists, capture the connection string for the container you want to expose through the File Manager.

At runtime the project registers an instance of `BlobServiceClient`, accesses containers via `BlobContainerClient`, and performs file operations through helper members such as `ReadAsync`, `CreateAsync`, `UploadAsync`, `UpdateAsync`, and `DeleteAsync`. Provide your storage credentials through the `ConnectionStrings:AzureStorage` setting (or user secrets/environment variables) so the sample can authenticate and hydrate the File Manager tree from your blobs.

If you prefer a local development story, keep the default `UseDevelopmentStorage=true` connection string and run [Azurite](https://learn.microsoft.com/azure/storage/common/storage-use-azurite) to emulate Blob Storage. Either way, the same provider flows ensure the File Manager UI always reflects whatever lives in your configured container.

## Local Setup
1. Clone this repository (`git clone https://github.com/telerik/ui-for-aspnet-core-examples.git`).
2. Navigate to `/KendoUI.FileManager.BlobStorage` and open `KendoUI.FileManager.BlobStorage.sln` in Visual Studio 2022 (17.x) or newer.
3. Clean the solution (Build → Clean Solution) to ensure NuGet packages restore fresh.
4. Set your Azure Blob Storage connection string in `KendoUI.FileManager.BlobStorage/KendoUI.FileManager.BlobStorage/appsettings.json` under `ConnectionStrings.AzureStorage` (or use `dotnet user-secrets set "ConnectionStrings:AzureStorage" "<your-connection-string>"`).
5. Build and run the solution (F5 or `dotnet run` from the `KendoUI.FileManager.BlobStorage` project folder) and browse to the hosted File Manager to start managing your blobs.
