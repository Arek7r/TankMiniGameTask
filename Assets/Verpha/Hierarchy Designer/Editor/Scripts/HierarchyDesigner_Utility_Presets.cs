#if UNITY_EDITOR
using UnityEngine;

namespace Verpha.HierarchyDesigner
{
    public static class HierarchyDesigner_Utility_Presets
    {
        #region Presets
        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset AzureDreamscapePreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Azure Dreamscape",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#318DCB"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.DefaultOutline,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#7EBCEF"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#3C5A81"),
                FontStyle.BoldAndItalic,
                13,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.DefaultFadedLeftAndRight,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#8E9FD5"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#8E9FD5"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#5A5485")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset BlackAndGoldPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Black and Gold",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1C1C1C"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFD102"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1C1C1C"),
                FontStyle.BoldAndItalic,
                13,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernI,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1C1C1C"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1C1C1C"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFC402")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset BlackAndWhitePreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Black and White",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#ffffff"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000"),
                FontStyle.Bold,
                12,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#ffffff80"),
                FontStyle.Italic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#ffffff80"),
                FontStyle.Italic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFF")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset BloodyMaryPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Bloody Mary",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#C50515E6"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFFE1"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#CF1625F0"),
                FontStyle.Bold,
                12,
                TextAnchor.UpperCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.DefaultFadedBottom,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFEEAA9C"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFEEAA9C"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFFC8")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset BlueHarmonyPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Blue Harmony",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#6AB1F8"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#A5D2FF"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#277DEC"),
                FontStyle.Bold,
                12,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernII,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#6AB1F8F0"),
                FontStyle.Bold,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#A5D2FF"),
                FontStyle.Bold,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#A5D2FF")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset DeepOceanPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Deep Ocean",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1E4E8A"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#041F54C8"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#041F54"),
                FontStyle.Bold,
                12,
                TextAnchor.LowerRight,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.DefaultFadedRight,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#0E244E"),
                FontStyle.Bold,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#0E244E"),
                FontStyle.Bold,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1E4E8A")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset DunesPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Dunes",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DDC0A4"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#E4C6AB"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AB673F"),
                FontStyle.Italic,
                13,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.DefaultFadedRight,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DDC0A4E1"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DDC0A4E1"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DDC0A4E1")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset MinimalBlackPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Minimal Black",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.DefaultOutline,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#3F3F3F"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000"),
                FontStyle.Bold,
                10,
                TextAnchor.MiddleLeft,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000C8"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000C8"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#000000F0")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset MinimalWhitePreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Minimal White",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFF"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.DefaultOutline,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#BEBEBE"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFF"),
                FontStyle.Bold,
                10,
                TextAnchor.MiddleLeft,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFFC8"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFFC8"),
                FontStyle.Italic,
                8,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFFFFFF0")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset NaturePreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Nature",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DFEAF0"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DFF6CA"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#70B879"),
                FontStyle.Normal,
                13,
                TextAnchor.MiddleLeft,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernII,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD9A5C8"),
                FontStyle.Normal,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD9A5C8"),
                FontStyle.Normal,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#BCD8E3")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset NavyBlueLightPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Navy Blue Light",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD6EC"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD6EC"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#113065"),
                FontStyle.Bold,
                12,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernII,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD6ECC8"),
                FontStyle.Bold,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD6ECC8"),
                FontStyle.Bold,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#AAD6EC")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset OldSchoolPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Old School",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#686868"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#00FF34"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#010101"),
                FontStyle.Normal,
                12,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1FC742F0"),
                FontStyle.Normal,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#1FC742F0"),
                FontStyle.Normal,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#686868")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset PrettyPinkPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Pretty Pink",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FB6B90"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#EFEBE0"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FB4570"),
                FontStyle.Bold,
                12,
                TextAnchor.MiddleLeft,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernII,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FB4570FA"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FB4570FA"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FB4570")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset RedDawnPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Red Dawn",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DF4148"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.Default,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FF5F2A"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#C00531"),
                FontStyle.BoldAndItalic,
                13,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.DefaultFadedLeftAndRight,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DF4148F0"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DF4148F0"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#DF4148")
            );
        }

        public static HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset SunflowerPreset()
        {
            return new HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset(
                "Sunflower",
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#F8B701"),
                HierarchyDesigner_Configurable_Folder.FolderImageType.ModernI,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#FFC80A"),
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#2A8FF3"),
                FontStyle.Bold,
                13,
                TextAnchor.MiddleCenter,
                HierarchyDesigner_Configurable_Separator.SeparatorImageType.ModernI,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#F8B701"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#F8B701"),
                FontStyle.BoldAndItalic,
                9,
                HierarchyDesigner_Shared_ColorUtility.HexToColor("#F8B701")
            );
        }
        #endregion

        #region Methods
        public static void ApplyPresetToFolders(HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset preset)
        {
            var foldersData = HierarchyDesigner_Configurable_Folder.GetAllFoldersData(false);
            foreach (string folderName in foldersData.Keys)
            {
                HierarchyDesigner_Configurable_Folder.SetFolderData(folderName, preset.folderColor, preset.folderImageType);
            }
        }

        public static void ApplyPresetToSeparators(HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset preset)
        {
            var separatorsData = HierarchyDesigner_Configurable_Separator.GetAllSeparatorsData(false);
            foreach (string separatorName in separatorsData.Keys)
            {
                HierarchyDesigner_Configurable_Separator.SetSeparatorData(separatorName, 
                    preset.separatorTextColor, 
                    preset.separatorBackgroundColor, 
                    preset.separatorFontSize, 
                    preset.separatorFontStyle, 
                    preset.separatorTextAlignment, 
                    preset.separatorBackgroundImageType);
            }
        }

        public static void ApplyPresetToTag(HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset preset)
        {
            HierarchyDesigner_Configurable_DesignSettings.TagColor = preset.tagTextColor;
            HierarchyDesigner_Configurable_DesignSettings.TagFontStyle = preset.tagFontStyle;
            HierarchyDesigner_Configurable_DesignSettings.TagFontSize = preset.tagFontSize;
        }

        public static void ApplyPresetToLayer(HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset preset)
        {
            HierarchyDesigner_Configurable_DesignSettings.LayerColor = preset.layerTextColor;
            HierarchyDesigner_Configurable_DesignSettings.LayerFontStyle = preset.layerFontStyle;
            HierarchyDesigner_Configurable_DesignSettings.LayerFontSize = preset.layerFontSize;
        }
        
       public static void ApplyPresetToTree(HierarchyDesigner_Configurable_Presets.HierarchyDesigner_Preset preset)
       {
           HierarchyDesigner_Configurable_DesignSettings.HierarchyTreeColor = preset.treeColor;
       }
       #endregion
    }
}
#endif