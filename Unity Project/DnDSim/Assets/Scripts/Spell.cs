using System.Collections.Generic;
using UnityEngine;

public class Spell {

    //Example of making a spell through code looks like:
    //Spell spell = new Spell("Acid Throw", 2, "A shimmering green arrow streaks toward a target within range and bursts in a spray of acid. Make a ranged spell attack against the target. On a hit, the target takes 4d4 acid damage immediately and 2d4 acid damage at the end of its next turn. On a miss, the arrow splashes the target with acid for half as much of the initial damage and no damage at the end of its next turn.", 90, "feet", 0, "", 1, true, true, true, "powdered rhubarb leaf and an adder’s stomach", 4, 4, 1, 0, "", 1, "action", "Evocation", new List<string>{"Wizard"});

    public string name; //The name of the spell
    public int level; //0 to 9 with 0 being a cantrip
    public string description; //What the spell does

    public int range; //0 if self/touch
    public string rangeUnit; //Touch, self, feet, miles
    public int effectRange; //Ex. in '30 foot cone', this would be the 30. (NOTE: All 'effect range' vars correspond to the 'Target' input field when making spells)
    public int effectRangeSize; //Ex. in '100 foot line that is 5 feet wide' this would be the 5
    public string effectRangeSizeUnit = "feet wide"; //Ex. in '100 foot line that is 5 feet wide' this would be the 'feet wide'
    public string effectRangeUnit; //Ex: foot-radius sphere, foot cone, etc
    public string effectRangeDescription; //Single Target, Multiple Targets, or AOE (NOTE: Picking a point in space without an area of effect would be Single Target. NOTE2: This var is the only effect range var used if a custom range is inputted and instead holds everything relating to the effect range in string form)
    public int numberOfTargets = 1; //If this spell can target more than one object this is how many it targets

    public bool isRitual; //If this spell is a ritual

    public bool cComponent; //If it requires concentration
    public bool vComponent; //If it uses verbal components
    public bool sComponent; //If it uses semantic components
    public string mComponentDescription; //Material component description; Ex: A tiny ball of bat guano and sulfur. (NOTE: If "" then the spell doesnt use material components)

    public int baseNumberOfDamageDice; //Ex: In 8d6, this would be the 8
    public int typeOfDamageDice; //Ex: In 8d6, this would be the 6
    public int diceNumberSpellSlotIncrease; //How many more dice to roll for each spell slot this is casted at above it's base level; Ex: In 'Increases by 1d6 for each slot above 3rd' this would be the 1
    public int typeOfDiceSpellSlotIncrease; //Which type of dice to add for each spell slot this is casted at above it's base level; Ex: In 'Increase by 1d6 for each slot above 3d', this would be the 6

    public int duration; //How long the spell lasts, 0 = instantaneous
    public string durationUnit; //Ex: Instantaneous, minutes, hours, days, etc

    public int castingTime; //How long it takes to cast the spell
    public string castingTimeUnit; //Ex: Action, bonus action, minutes, etc

    public string school; //One of 8: Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, and Transmutation

    public string savingThrow; //One of 6: Strength, Dexterity, Constitution, Intelligence, Wisdom, and Charisma; (NOTE: If the spell doesnt require one it will be set to 'None')

    public Spell(string name, int level, string description, int range, string rangeUnit, int effectRange, string effectRangeUnit, int numberOfTargets, bool vComponent, bool sComponent, bool mComponent, bool cComponent, string componentDescription, int baseNumberOfDamageDice, int typeOfDamageDice, int diceNumberSpellSlotIncrease, int spellDuration, string spellDurationUnit, int castingTime, string castingTimeUnit, string school, List<string> lists, bool isRitual, string savingThrow)
    {
        //NOTE: THIS IS NO LONGER BEING UPDATED AND MAY NOT FUNCTION CORRECTLY, WILL BE FADED OUT WHEN THE INGAME SPELL CREATOR IS FINISHED
        this.name = name;
        this.range = range;
        this.description = description;
        this.rangeUnit = rangeUnit;
        this.effectRange = effectRange;
        this.effectRangeUnit = effectRangeUnit;
        this.numberOfTargets = numberOfTargets;
        this.cComponent = cComponent;
        this.vComponent = vComponent;
        this.sComponent = sComponent;
        this.mComponentDescription = componentDescription;
        this.baseNumberOfDamageDice = baseNumberOfDamageDice;
        this.typeOfDamageDice = typeOfDamageDice;
        this.diceNumberSpellSlotIncrease = diceNumberSpellSlotIncrease;
        this.castingTime = castingTime;
        this.castingTimeUnit = castingTimeUnit;
        this.school = school;
        this.isRitual = isRitual;
        this.savingThrow = savingThrow;
    }

    public Spell()
    {
        this.name = "Undefined";
    }
}
