﻿namespace Refman.ViewModels
{
    using Caliburn.Micro;

    using Refman.Factories.Interfaces;
    using Refman.Services.Interfaces;
    using Refman.ViewModels.Interfaces;

    internal class FileSystemViewModel : ViewModelBase, IFileSystemViewModel
    {
        private readonly IFileSystemFactory _fileSystemFactory;

        private readonly IFileSystemService _fileSystemService;

        public FileSystemViewModel(IFileSystemFactory fileSystemFactory, IFileSystemService fileSystemService, ISettingsService settingsService)
        {
            _fileSystemFactory = fileSystemFactory;
            _fileSystemService = fileSystemService;

            PopulateRoot();

            settingsService["References Path"].ValueChanged += (sender, e) => PopulateRoot();
        }

        public IObservableCollection<IFolderViewModel> RootFolderCollection { get; } = new BindableCollection<IFolderViewModel>();

        private void PopulateRoot()
        {
            IFolderViewModel rootFolder = _fileSystemFactory.MakeFolder(_fileSystemService.ReadRootFolder());

            rootFolder.IsExpanded = true;

            RootFolderCollection.Clear();
            RootFolderCollection.Add(rootFolder);
        }
    }
}