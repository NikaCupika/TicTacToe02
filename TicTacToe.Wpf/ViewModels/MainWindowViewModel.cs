using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using TicTacToe.Core;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.Cordinates;
using TicTacToe.Domain.Models.Players;
using TicTacToe.Domain.Models.Players.Bots;

namespace TicTacToe.Wpf.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private GameLogic? _game;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartClickCommand))]
    private string? _player01Name;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartClickCommand))]
    private Type? _player01Type = typeof(HumanPlayer);

    partial void OnPlayer01TypeChanged(Type? value)
    {
        if ((value == typeof(EasyBot) || value == typeof(HardBot)) && string.IsNullOrEmpty(Player01Name))
        {
            Player01Name = GameAccessLayer.GetNameFromType(value);
        }
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartClickCommand))]
    private string? _player02Name;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartClickCommand))]
    private Type? _player02Type = typeof(HumanPlayer);
    partial void OnPlayer02TypeChanged(Type? value)
    {
        if ((value == typeof(EasyBot) || value == typeof(HardBot)) && string.IsNullOrEmpty(Player02Name))
        {
            Player02Name = GameAccessLayer.GetNameFromType(value);
        }
    }

    [ObservableProperty]
    private string[] _field = new string[9];

    public Dictionary<string, Type> PlayerTypes => GameAccessLayer.GetPlayerTypes();

    public string Message
    {
        get
        {
            if (_game == null) return "Vergebe die Namen und starte das Spiel";
            if (_game.GameOver && _game.CurrentPlayer == null) return "Es ist unentschieden";
            if (_game.GameOver) return $"{_game.CurrentPlayer?.Symbol} ({_game.CurrentPlayer?.Name}) hat gewonnen!";
            return $"{_game.CurrentPlayer?.Symbol} ({_game.CurrentPlayer?.Name})";
        }
    }

    public bool NotPlaying => _game == null || _game.GameOver;

    private bool _playersNotEmpty => !string.IsNullOrEmpty(Player01Name) && Player01Type is not null && !string.IsNullOrEmpty(Player02Name) && Player02Type is not null;

    [RelayCommand(CanExecute = nameof(_playersNotEmpty))]
    public void StartClick()
    {
        BasePlayer player01 = GameAccessLayer.CreatePlayer(Player01Name!, Player01Type!);
        BasePlayer player02 = GameAccessLayer.CreatePlayer(Player02Name!, Player02Type!);
        _game = new(player01, player02);

        UpdateBoard();
    }

    private bool FieldIsClickable(object args)
    {
        // game is playing & given argument is number
        if (_game == null || _game.GameOver || args is not string || !int.TryParse(args.ToString(), out int parm))
        {
            return false;
        }

        Coordinate coords = GameAccessLayer.GetCoordinateFromField(parm);
        return _game.GetValue(coords) == default;
    }

    [RelayCommand(CanExecute = nameof(FieldIsClickable))]
    public void FieldClick(object args)
    {
        // game is playing & given argument is number
        if (_game == null || _game.GameOver || args is not string || !int.TryParse(args.ToString(), out int parm))
        {
            return;
        }

        Coordinate coords = GameAccessLayer.GetCoordinateFromField(parm);
        _game.SetValue(coords);

        UpdateBoard();
    }

    private void UpdateBoard()
    {
        Field = new string[9];
        for (int i = 0; i < 9; i++)
        {
            Coordinate coords = GameAccessLayer.GetCoordinateFromField(i);
            Field[i] = _game!.GetValue(coords).ToString();
        }

        OnPropertyChanged(nameof(Field));
        FieldClickCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(Message));
        OnPropertyChanged(nameof(NotPlaying));
    }
}
