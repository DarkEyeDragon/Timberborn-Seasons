﻿using FloodSeason.Exceptions;
using FloodSeason.WeatherLogic.Modifiers;

namespace FloodSeason.WeatherLogic;

public class Weather
{
    public WeatherType WeatherType { get; }
    public IModifier Modifier { get; }


    public Weather(WeatherType weatherType, IModifier modifier)
    {
        WeatherType = weatherType;
        Modifier = modifier;
    }

    public override string ToString()
    {
        return $"{WeatherType}";
    }
}