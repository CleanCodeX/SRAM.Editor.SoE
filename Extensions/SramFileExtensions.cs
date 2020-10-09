using System;
using SramFormat.SoE;
using SramFormat.SoE.Models.Enums;

namespace SramEditor.SoE.Extensions
{
	public static class SramFileExtensions
    {
        #region CharacterName

        public static string GetBoyName(this CurrentGameSramFileSoE source, Character character)
        {
            var name = character == Character.Boy
                ? source.CurrentGame.BoyName.StringValue
                : source.CurrentGame.DogName.StringValue;

            switch (source.Region)
            {
                case FileRegion.Germany:
                    name = name.Replace((char)0xCB, (char)0xC4); // fix A umlaut
                    name = name.Replace((char)0xDB, (char)0xD6); // fix O umlaut
                    name = name.Replace((char)0xDF, (char)0xDC); // fix U umlaut
                    name = name.Replace((char)0xE3, (char)0xE4); // fix a umlaut
                    name = name.Replace((char)0xEF, (char)0xF6); // fix o umlaut
                    name = name.Replace((char)0xF3, (char)0xFC); // fix u umlaut
                    name = name.Replace((char)0xC6, (char)0xDF); // fix eszett
                    break;
                case FileRegion.Spain:
                    name = name.Replace((char)0xD7, (char)0xF1); // fix n tilde
                    break;
            }

            return name;
        }

        public static void SetBoyName(this CurrentGameSramFileSoE source, Character character, string name)
        {
            switch (source.Region)
            {
                case FileRegion.Germany:
                    name = name.Replace((char)0xC4, (char)0xCB); // fix A umlaut
                    name = name.Replace((char)0xD6, (char)0xDB); // fix O umlaut
                    name = name.Replace((char)0xDC, (char)0xDF); // fix U umlaut
                    name = name.Replace((char)0xE4, (char)0xE3); // fix a umlaut
                    name = name.Replace((char)0xF6, (char)0xEF); // fix o umlaut
                    name = name.Replace((char)0xFC, (char)0xF3); // fix u umlaut
                    name = name.Replace((char)0xDF, (char)0xC6); // fix eszett
                    break;
                case FileRegion.Spain:
                    name = name.Replace((char)0xF1, (char)0xD7); // fix n tilde
                    break;
            }

            if (character == Character.Boy)
                source.CurrentGame.BoyName.StringValue = name;
            else
                source.CurrentGame.DogName.StringValue = name;
        }

        #endregion

        #region Charm

        public static bool HasCharm(this CurrentGameSramFileSoE source, Charm value)
        {
            source.IsValid();

            return source.CurrentGame.Charms.EnumValue.HasFlag(value);
        }

        public static void SetCharm(this CurrentGameSramFileSoE source, Charm value, bool have)
        {
            source.IsValid();

            if (have)
                source.CurrentGame.Charms.EnumValue |= value;
            else
                source.CurrentGame.Charms.EnumValue &= ~value;

            source.IsModified = true;
        }

        #endregion

        #region Alchemy

        public static bool HasAlchemy(this CurrentGameSramFileSoE source, Alchemy value)
        {
            source.IsValid();

            return source.CurrentGame.Alchemies.EnumValue.HasFlag(value);
        }

        public static void SetAlchemy(this CurrentGameSramFileSoE source, Alchemy value, bool have)
        {
            source.IsValid();

            if (have)
                source.CurrentGame.Alchemies.EnumValue |= value;
            else
                source.CurrentGame.Alchemies.EnumValue &= ~value;

            source.IsModified = true;
        }

        #endregion

        #region Weapons

        public static bool HasWeapon(this CurrentGameSramFileSoE source, Weapon value)
        {
            source.IsValid();

            return source.CurrentGame.Weapons.EnumValue.HasFlag(value);
        }

        public static void SetWeapon(this CurrentGameSramFileSoE source, Weapon value, bool have)
        {
            source.IsValid();

            if (have)
                source.CurrentGame.Weapons.EnumValue |= value;
            else
                source.CurrentGame.Weapons.EnumValue &= ~value;

            source.IsModified = true;
        }

        #endregion

        #region Moneys

        public static uint GetMoney(this CurrentGameSramFileSoE source, Money value)
        {
            source.IsValid();

            return value switch
            {
                Money.Talons => source.CurrentGame.Moneys.Talons.Value,
                Money.Jewels => source.CurrentGame.Moneys.Talons.Value,
                Money.GoldCoins => source.CurrentGame.Moneys.GoldCoins.Value,
                Money.Credits => source.CurrentGame.Moneys.Credits.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        public static void SetMoney(this CurrentGameSramFileSoE source, Money money, uint value)
        {
            source.IsValid();

            switch (money)
            {
                case Money.Talons:
                    source.CurrentGame.Moneys.Talons.Value = value;
                    break;
                case Money.Jewels:
                    source.CurrentGame.Moneys.Jewels.Value = value;
                    break;
                case Money.GoldCoins:
                    source.CurrentGame.Moneys.GoldCoins.Value = value;
                    break;
                case Money.Credits:
                    source.CurrentGame.Moneys.Credits.Value = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            source.IsModified = true;
        }

        #endregion
    }
}
