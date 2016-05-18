﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace CustomShops
{
	public class ConsumableProductDecorator : IDecorator
	{
		private string _name;
		public ConsumableProductDecorator (string name)
		{
			this._name = name;
		}

		public void Decorate(GameObject go, Dictionary<string, object> options, AssetBundle assetBundle)
		{
			var consumable = go.AddComponent<ConsumableProductInstance> ();

			BindingFlags flags = BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic;
			typeof(Product).GetField ("displayName", flags).SetValue (consumable, _name);

			consumable.defaultPrice = (float)(double)options ["price"];

            if (options.ContainsKey ("hand")) {
                switch ((string)options ["hand"]) {
                case "left":
                    consumable.handSide = Hand.Side.LEFT;
                    break;
                case "right":
                    consumable.handSide = Hand.Side.RIGHT;
                    break;
                }
            } else {
                consumable.handSide = Hand.Side.LEFT;
            }


			if (options.ContainsKey ("consumeanimation")) {
				switch ((string)options ["consumeanimation"]) {
				case "generic":
					consumable.consumeAnimation = ConsumableProduct.ConsumeAnimation.GENERIC;
					break;
				case "drink_straw":
					consumable.consumeAnimation = ConsumableProduct.ConsumeAnimation.DRINK_STRAW;
					break;
				case "lick":
					consumable.consumeAnimation = ConsumableProduct.ConsumeAnimation.LICK;
					break;
				case "with_hands":
					consumable.consumeAnimation = ConsumableProduct.ConsumeAnimation.WITH_HANDS;
					break;

				}
			} else {
				consumable.consumeAnimation = ConsumableProduct.ConsumeAnimation.GENERIC;
			}

			if (options.ContainsKey ("temperature")) {
				switch ((string)options ["temperature"]) {
				case "none":
					consumable.temperaturePreference = ConsumableProduct.TemperaturePreference.NONE;
					break;
				case "cold":
					consumable.temperaturePreference = ConsumableProduct.TemperaturePreference.COLD;
					break;
				case "hot":
					consumable.temperaturePreference = ConsumableProduct.TemperaturePreference.HOT;
					break;
				}
			} else {
				consumable.temperaturePreference = ConsumableProduct.TemperaturePreference.NONE;
			}
			consumable.portions = (int)(Int64)options ["portions"];


            if (options.ContainsKey ("modeltrash")) {
                switch ((string)options ["modeltrash"]) {
                case "PopCanTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.PopCanTrash).gameObject;
                    break;
                case "TeaTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.TeaTrash).gameObject;
                    break;
                case "CandyTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.CandyTrash).gameObject;
                    break;
                case "SoftDrinkTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.SoftDrinkTrash).gameObject;
                    break;
                case "SnowconeTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.SnowconeTrash).gameObject;
                    break;
                case "BubbleTeaTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.BubbleTeaTrash).gameObject;
                    break;
                case "ChipBagTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.ChipBagTrash).gameObject;
                    break;
                case "MiniDonutsTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.MiniDonutsTrash).gameObject;
                    break;
                case "ChineseFoodTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.ChineseFoodTrash).gameObject;
                    break;
                case "PopcornTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.PopcornTrash).gameObject;
                    break;
                case "FriesTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.FriesTrash).gameObject;
                    break;
                case "HotDrinkTrash":
                    consumable.trash = AssetManager.Instance.getPrefab (Prefabs.HotDrinkTrash).gameObject;
                    break;
                default:
                    var asset = UnityEngine.Object.Instantiate (assetBundle.LoadAsset ((string)options ["modeltrash"])) as GameObject;
                    asset.gameObject.SetActive (false);
                    Trash t = asset.AddComponent<TrashInstance> ();
                    AssetManager.Instance.registerObject (t);
                    consumable.trash = asset;
                    break;

                }
            }
		}
	}
}

