﻿using Peernet.Browser.Plugins.ByteViewer.ViewModels;
using Peernet.Browser.Plugins.ByteViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.ByteViewer.Services
{
    public class ByteViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public ByteViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new ByteViewerViewModel();
            var stream = await fileClient.Read(Convert.ToHexString(file.Hash), Convert.ToHexString(file.NodeId));
            await viewModel.Prepare(new ByteViewerViewModelParameter { FileStream = stream, Title = file.Name });
            new ByteViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Type is LowLevelFileType.Binary)
            {
                return true;
            }
             
            return false;
        }
    }
}