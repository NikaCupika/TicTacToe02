using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Wpf.ViewModels;

public static class GameAccessLayer
{
    public static Dictionary<string, Type> GetPlayerTypes() 
        => typeof(BasePlayer)
            .Assembly.GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => x.IsSubclassOf(typeof(BasePlayer)))
            .ToDictionary(x => x.Name, x => x);

    public static BasePlayer CreatePlayer(string name, Type type)
    {
        BasePlayer player = (BasePlayer)Activator.CreateInstance(type)!;
        player.Name = name;
        return player;
    }
    
    public static Coordinate GetCoordinatesOfField(string parms)
    {
        if(!int.TryParse(parms, out int coord))
        {
            throw new Exception();
        }

        var idk = new Coordinate(coord % 3, coord / 3);
        return idk;
    }
}
