using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockItemsCreateBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(2000);
        }

        protected async Task HandleValidSubmit()
        {
            await Task.Delay(2000);
            StatusClass = "alert-success";
            Message = "Comment successfully.";
            Saved = true;
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateBack()
        {
            NavigationManager.NavigateTo("");
        }
    }
}