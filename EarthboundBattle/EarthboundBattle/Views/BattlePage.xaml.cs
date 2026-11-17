using EarthboundBattle.ViewModels;

namespace EarthboundBattle.Views;

public partial class BattlePage : ContentPage
{
    public BattlePage(BattleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}