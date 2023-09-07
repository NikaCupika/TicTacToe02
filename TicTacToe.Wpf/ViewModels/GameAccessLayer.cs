using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Wpf.ViewModels;

public static class GameAccessLayer
{
    public static Dictionary<string, Type> GetPlayerTypes()
    {
        IEnumerable<Type> types = typeof(BasePlayer)
                        .Assembly.GetTypes()
                        .Where(x => !x.IsAbstract)
                        .Where(x => x.IsSubclassOf(typeof(BasePlayer)));

        Dictionary<string, Type> result = new();
        foreach (Type type in types)
        {
            result.Add(GetNameFromType(type), type);
        }

        return result;
    }

    public static string GetNameFromType(Type type) => (Activator.CreateInstance(type) as BasePlayer)!.Name;

    public static BasePlayer CreatePlayer(string name, Type type)
    {
        BasePlayer player = (BasePlayer)Activator.CreateInstance(type)!;
        player.Name = name;
        return player;
    }

    public static Coordinate GetCoordinateFromField(int coord) => new Coordinate(coord % 3, coord / 3);
}
