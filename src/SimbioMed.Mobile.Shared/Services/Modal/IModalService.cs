﻿using System.Threading.Tasks;
using SimbioMed.Views;
using Xamarin.Forms;

namespace SimbioMed.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
