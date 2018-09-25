using System.Collections.Generic;
using UnityEngine;

public class Spell {

    //Example of making a spell through code looks like:
    //Spell spell = new Spell("Acid Throw", 2, "A shimmering green arrow streaks toward a target within range and bursts in a spray of acid. Make a ranged spell attack against the target. On a hit, the target takes 4d4 acid damage immediately and 2d4 acid damage at the end of its next turn. On a miss, the arrow splashes the target with acid for half as much of the initial damage and no damage at the end of its next turn.", 90, "feet", 0, "", 1, true, true, true, "powdered rhubarb leaf and an adder’s stomach", 4, 4, 1, 0, "", 1, "action", "Evocation", new List<string>{"Wizard"});

    public string name; //The name of the spell
    public int level; //0 to 9 with 0 being a cantrip
    public string description; //What the spell does

    public int range; //0 = touch
    public string rangeUnit; //Feet, yards, etc
    public int effectRange; //0 = target
    public string effectRangeUnit; //Ex: foot-radius
    public int numberOfTargets; //If this spell can target more than one creature, 0 = area of effect attack / choosing a point instead of creature

    public bool vComponent; //If it uses verbal components
    public bool sComponent; //If it uses semantic components
    public bool mComponent; //If it uses material components
    public string componentDescription; //Ex: A tiny ball of bat guano and sulfur

    public int baseNumberOfDamageDice; //Ex: In 8d6, this would be the 8
    public int typeOfDamageDice; //Ex: In 8d6, this would be the 6
    public int diceNumberSpellSlotIncrease; //How many more dice to roll for each spell slot this is casted at above it's base level; Ex: Increases by 1d6 for each slot above 3rd

    public int spellDuration; //How long the spell lasts, 0 = instantaneous
    public string spellDurationUnit; //Ex: minutes, hours, days, etc

    public int castingTime; //How long it takes to cast the spell
    public string castingTimeUnit; //Ex: Instantaneous, actions, minutes, etc

    public string school; //One of 8: Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, and Transmutation
    public List<string> lists; //Ex: Wizard, Bard, Hexblade, etc

    public Spell(string name, int level, string description, int range, string rangeUnit, int effectRange, string effectRangeUnit, int numberOfTargets, bool vComponent, bool sComponent, bool mComponent, string componentDescription, int baseNumberOfDamageDice, int typeOfDamageDice, int diceNumberSpellSlotIncrease, int spellDuration, string spellDurationUnit, int castingTime, string castingTimeUnit, string school, List<string> lists)
    {
        this.name = name;
        this.range = range;
        this.description = description;
        this.rangeUnit = rangeUnit;
        this.effectRange = effectRange;
        this.effectRangeUnit = effectRangeUnit;
        this.numberOfTargets = numberOfTargets;
        this.vComponent = vComponent;
        this.sComponent = sComponent;
        this.mComponent = mComponent;
        this.componentDescription = componentDescription;
        this.baseNumberOfDamageDice = baseNumberOfDamageDice;
        this.typeOfDamageDice = typeOfDamageDice;
        this.diceNumberSpellSlotIncrease = diceNumberSpellSlotIncrease;
        this.spellDuration = spellDuration;
        this.spellDurationUnit = spellDurationUnit;
        this.castingTime = castingTime;
        this.castingTimeUnit = castingTimeUnit;
        this.school = school;
        this.lists = lists;
    }

    public Spell()
    {
        this.name = "Undefined";
    }
}
