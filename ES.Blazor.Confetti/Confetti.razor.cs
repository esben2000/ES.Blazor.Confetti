using Microsoft.AspNetCore.Components;

namespace ES.Blazor.Confetti;

public partial class Confetti
{
    [Parameter]
    public bool Visible { get; set; }

    public async Task ShowAsync(double duration = 0)
    {
        Visible = true;
        await InvokeAsync(StateHasChanged);

        if (duration > 0)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(duration));

            while (await timer.WaitForNextTickAsync())
            {
                Visible = false;
                timer.Dispose();
            }

            await InvokeAsync(StateHasChanged);
        }
    }

    public void Show()
    {
        Visible = true;
        StateHasChanged();
    }

    public async Task HideAsync()
    {
        Visible = false;
        await InvokeAsync(StateHasChanged);
    }

    public void Hide()
    {
        Visible = false;
        StateHasChanged();
    }
}