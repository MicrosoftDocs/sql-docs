---
title: Revert word breakers used by Full-Text Search to previous version
titleSuffix: SQL Server Full-Text Search
description: Revert legacy Full-Text Search word breakers to previous versions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/08/2025
ms.service: sql
ms.subservice: search
ms.topic: how-to
monikerRange: ">=sql-server-2017"
---
# Revert word breakers used by Full-Text Search to previous version

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the legacy procedure for switching word breakers between the updated components introduced in [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and the previous components.

For information about the full-text index version change in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], see [Full-text index version upgrade](full-text-index-version-upgrade.md).

[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] installs and enables a version of the word breakers and stemmers for all languages supported by Full-Text Search, except for Korean. This article describes how to switch from this version of these components to the previous version, or to switch back from the previous version to the new version.

This article doesn't discuss the following languages:

| Language | Description |
| --- | --- |
| **English** | To revert or restore the English components, see [Change the word breaker used for US English and UK English](change-the-word-breaker-used-for-us-english-and-uk-english.md). |
| **Danish, Polish, and Turkish** | The third-party word breakers for Danish, Polish, and Turkish that were included with previous releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are replaced with [!INCLUDE [msCoName](../../includes/msconame-md.md)] components. |
| **Czech and Greek** | There are new word breakers for Czech and Greek. Previous releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Full-Text Search didn't include support for these two languages. |
| **Korean** | The word breaker and stemmer for the Korean language aren't upgraded in this release. |

For general information about word breakers and stemmers, see [Configure and manage word breakers and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md).

<a id="overview"></a>

## Overview of reverting and restoring word breakers and stemmers

The instructions for reverting and restoring word breakers and stemmers depend on the language. The following table summarizes the three sets of actions that might be required to revert to the previous version of the components.

> [!CAUTION]  
> [!INCLUDE [ssnoteregistry-md](../../includes/ssnoteregistry-md.md)]

| Current file | Previous file | Number of affected languages | Action for files | Action for registry entries |
| --- | --- | --- | --- | --- |
| `NaturalLanguage6.dll` | `NaturalLanguage6.dll` | 34 | Get and install a previous version of `NaturalLanguage6.dll`, overwriting the current version of the file. | No action required.<br /><br />The registry keys and values didn't change for this release. |
| (Other file name) | `NaturalLanguage6.dll` | 5 | Get and install a previous version of `NaturalLanguage6.dll`, overwriting the current version of the file. | Change a set of registry entries to specify the previous version of the components. |
| (Other file name) | (Other file name) | 6 | No action required.<br /><br />[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] setup copies both the current and the previous versions of the components to the `Binn` folder. | Change a set of registry entries to specify the previous version of the components. |

> [!WARNING]  
> If you replace the current version of the file `NaturalLanguage6.dll` with a different version, then the behavior of all the languages that use this file changes.

The files described in this article are DLL files that are installed in the `MSSQL\Binn` folder for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. The full path is typically the following path:

```output
C:\Program Files\Microsoft SQL Server\<instance>\MSSQL\Binn
```

<a id="nl6nl6"></a>

## Languages for which the file name of both the current and previous word breaker is NaturalLanguage6.dll

For the languages in the following table, the file name of both the current and previous word breaker is `NaturalLanguage6.dll`. To revert or restore these components, you must overwrite `NaturalLanguage6.dll` with a different version of the same file. You don't need to change any registry entries, because the registry entries didn't change for this release.

> [!WARNING]  
> If you replace the current version of the file `NaturalLanguage6.dll` with a different version, then the behavior of all the languages that use this file changes.

### List of affected languages

| Language | Abbreviation used in the registry | Locale identifier (LCID) |
| --- | --- | --- |
| Bengali | `ben` | 1093 |
| Bulgarian | `bgr` | 1026 |
| Catalan | `cat` | 1027 |
| Spanish | `esn` | 3082 |
| French | `fra` | 1036 |
| Gujarati | `guj` | 1095 |
| Hebrew | `heb` | 1037 |
| Hindi | `hin` | 1081 |
| Croatian | `hrv` | 1050 |
| Indonesian | `ind` | 1057 |
| Icelandic | `isl` | 1039 |
| Italian | `ita` | 1040 |
| Kannada | `kan` | 1099 |
| Lithuanian | `lth` | 1063 |
| Latvian | `lvi` | 1062 |
| Malayalam | `mal` | 1100 |
| Marathi | `mar` | 1102 |
| Malay | `msl` | 1086 |
| Neutral | `Neutral` | 0000 |
| Norwegian Bokmål | `nor` | 1044 |
| Punjabi | `pan` | 1094 |
| Portuguese (Brazil) | `ptb` | 1046 |
| Portuguese | `ptg` | 2070 |
| Romanian | `rom` | 1048 |
| Slovak | `sky` | 1051 |
| Slovenian | `slv` | 1060 |
| Serbian - Cyrillic | `srb` | 3098 |
| Serbian - Latin | `srl` | 2074 |
| Swedish | `sve` | 1053 |
| Tamil | `tam` | 1097 |
| Telugu | `tel` | 1098 |
| Ukrainian | `ukr` | 1058 |
| Urdu | `urd` | 1056 |
| Vietnamese | `vit` | 1066 |

The preceding table is sorted alphabetically on the Abbreviation column.

<a id="nl6nl6revert"></a>
<a id="nl6nl6restore"></a>

### [Revert to the previous components](#tab/revert)

1. Go to the `Binn` folder described earlier.

1. Back up the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of `NaturalLanguage6.dll` to another location.

1. Copy the previous version of `NaturalLanguage6.dll` from the `Binn` folder of an instance of [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] or [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] into the `Binn` folder of the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] instance.

   > [!WARNING]  
   > This change affects all the languages that use `NaturalLanguage6.dll` in both the current and previous version.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### [Restore the current components](#tab/restore)

1. Go to the location where you backed up the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of `NaturalLanguage6.dll`.

1. Copy the current version of `NaturalLanguage6.dll` from the backup location into the `Binn` folder of the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] instance.

   > [!WARNING]  
   > This change affects all the languages that use `NaturalLanguage6.dll` in both the current and previous version.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

---

<a id="newnl6"></a>

## Languages for which the file name of the previous word breaker is NaturalLanguage6.dll

For the languages in the following table, the file name of the previous word breaker is different from the file name of the new version. The previous file name is `NaturalLanguage6.dll`. To revert to the previous version, you must overwrite the current version of `NaturalLanguage6.dll` with an earlier version of the same file. You must also change a set of registry entries to specify the previous or current version of the components.

> [!WARNING]  
> If you replace the current version of the file `NaturalLanguage6.dll` with a different version, then the behavior of all the languages that use this file changes.

### List of affected languages

| Language | Abbreviation<br />used in the<br />registry | LCID |
| --- | --- | --- |
| Arabic | `ara` | 1025 |
| German | `deu` | 1031 |
| Japanese | `jpn` | 1041 |
| Dutch | `nld` | 1043 |
| Russian | `rus` | 1049 |

The preceding table is sorted alphabetically on the Abbreviation column.

Use the following instructions together with the list of values in the section [File names and registry values for reverting and restoring word breakers and stemmers](#newnl6values).

<a id="newnl6revert"></a>
<a id="newnl6restore"></a>

### [Revert to the previous components](#tab/revert)

1. Go to the `Binn` folder described earlier.

1. Don't remove the files for the current version of the components from the `Binn` folder.

1. Back up the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of `NaturalLanguage6.dll` to another location.

1. Copy the previous version of `NaturalLanguage6.dll` from the `Binn` folder of an instance of [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] or [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] into the `Binn` folder of the new [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] instance.

   > [!WARNING]  
   > This change affects all the languages that use `NaturalLanguage6.dll` in both the current and previous version.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID`.

1. Use the following steps to add new keys for the COM ClassIDs for the previous word breaker and stemmer interfaces for the selected language:

   1. Add a new key with the value from the table for the previous word breaker.

   1. Update the (Default) data of that key value to the file name of the previous word breaker from the table.

   1. If the selected language uses a stemmer, add a new key with the value from the table for the previous stemmer.

   1. If the selected language uses a stemmer, update the (Default) data of that key value to the file name of the previous stemmer from the table.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>`. `<language_key>` represents the abbreviation for the language that is used in the registry; for example, `fra` for French and `esn` for Spanish.

1. Update the `WBreakerClass` key value to the value from the table for the current word breaker.

1. If the selected language uses a stemmer, update the `StemmerClass` key value to the value from the table for the current stemmer.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### [Restore the current components](#tab/restore)

1. Go to the location where you backed up the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of `NaturalLanguage6.dll`.

1. Copy the current version of `NaturalLanguage6.dll` from the backup location into the `Binn` folder of the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] instance.

   > [!WARNING]  
   > This change affects all the languages that use `NaturalLanguage6.dll` in both the current and previous version.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID`.

1. If the following keys don't exist, use the following steps to add new keys for the COM ClassIDs for the current word breaker and stemmer interfaces for the selected language:

   1. Add a new key with the value from the table for the current word breaker.

   1. Update the (Default) data of that key value to the file name of the current word breaker from the table.

   1. If the selected language uses a stemmer, add a new key with the value from the table for the current stemmer.

   1. If the selected language uses a stemmer, update the (Default) data of that key value to the file name of the current stemmer from the table.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>`. `<language_key>` represents the abbreviation for the language that is used in the registry; for example, `fra` for French and `esn` for Spanish.

1. Update the `WBreakerClass` key value to the value from the table for the previous word breaker.

1. If the selected language uses a stemmer, update the `StemmerClass` key value to the value from the table for the previous stemmer.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

---

<a id="newnl6values"></a>

### File names and registry values for reverting and restoring word breakers and stemmers

Use the following list of file names and registry entries together with the instructions in the preceding section. Use the previous values to revert to the previous version, or use the current values to restore the current version of the components.

The following list is sorted alphabetically on the abbreviation used for each language.

#### Arabic (`ara`) - LCID 1025

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `7efd3c7e-9e4b-4a93-9503-decd74c0ac6d` | `483b0283-25db-4c92-9c15-a65925cb95ce` |
| Previous file name | `NaturalLanguage6.dll` | `NaturalLanguage6.dll` |
| Current CLSID | `04b37e30-c9a9-4a7d-8f20-792fc87ddf71` | None |
| Current file name | `MSWB7.dll` | None |

#### German (`deu`) - LCID 1031

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `45eaca36-dbe9-4e4a-a26d-5c201902346d` | `65170ae4-0ad2-4fa5-b3ba-7cd73e2da825` |
| Previous file name | `NaturalLanguage6.dll` | `NaturalLanguage6.dll` |
| Current CLSID | `dfa00c33-bf19-482e-a791-3c785b0149b4` | `8a474d89-6e2f-419c-8dd5-9b50edc8c787` |
| Current file name | `MsWb7.dll` | `MsWb7.dll` |

#### Japanese (`jpn`) - LCID 1041

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `e1e8f15e-8bec-45df-83bf-50ff84d0cab5` | `3d5df14f-649f-4cbc-853d-f18fede9cf5d` |
| Previous file name | `NaturalLanguage6.dll` | `NaturalLanguage6.dll` |
| Current CLSID | `04096682-6ece-4e9e-90c1-52d81f0422ed` | None |
| Current file name | `MsWb70011.dll` | None |

#### Dutch (`nld`) - LCID 1043

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `2c9f6beb-c5b0-42b6-a5ee-84c24dc0d8ef` | `f7a465ee-13fb-409a-b878-195b420433af` |
| Previous file name | `NaturalLanguage6.dll` | `NaturalLanguage6.dll` |
| Current CLSID | `69483c30-a9af-4552-8f84-a0796ad5285b` | `cf923cb5-1187-43ab-b053-3e44bed65ffa` |
| Current file name | `MsWb7.dll` | `MsWb7.dll` |

#### Russian (`rus`) - LCID 1049

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `2cb6cda4-1c14-4392-a8ec-81eef1f2e079` | `e06a0ddd-e81a-4e93-8a8d-f386c3a1b670` |
| Previous file name | `NaturalLanguage6.dll` | `NaturalLanguage6.dll` |
| Current CLSID | `aaa3d3bd-6de7-4317-91a0-d25e7d3babc3` | `d42c8b70-adeb-4b81-a52f-c09f24f77dfa` |
| Current file name | `MsWb7.dll` | `MsWb7.dll` |

<a id="newnew"></a>

## Languages for which the previous and current file name isn't NaturalLanguage6.dll

For the languages in the following table, the file names of the previous word breakers and stemmers differ from the file names of the new versions. Both the previous and the current file name isn't `NaturalLanguage6.dll`. You don't need to replace any files, because [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] setup copies both the current and the previous versions of the components to the `Binn` folder. However, you must change a set of registry entries to specify the previous or current version of the components.

### List of affected languages

| Language | Abbreviation used in the registry | LCID |
| --- | --- | --- |
| Simplified Chinese | `chs` | 2052 |
| Traditional Chinese | `cht` | 1028 |
| Thai | `tha` | 1054 |
| Chinese Traditional | `zh-hk` | 3076 |
| Chinese Traditional | `zh-mo` | 5124 |
| Chinese Simplified | `zh-sg` | 4100 |

The preceding table is sorted alphabetically on the Abbreviation column.

Use the following instructions together with the list of values in the section [File names and registry values for reverting and restoring word breakers and stemmers](#newnewvalues).

<a id="newnewrevert"></a>
<a id="newnewrestore"></a>

### [Revert to the previous components](#tab/revert)

1. Don't remove the files for the current version of the components from the `Binn` folder.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID`.

1. Use the following steps to add new keys for the COM ClassIDs for the previous word breaker and stemmer interfaces for the selected language:

   1. Add a new key with the value from the table for the previous word breaker.

   1. Update the (Default) data of that key value to the file name of the previous word breaker from the table.

   1. If the selected language uses a stemmer, add a new key with the value from the table for the previous stemmer.

   1. If the selected language uses a stemmer, update the (Default) data of that key value to the file name of the previous stemmer from the table.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>`. `<language_key>` represents the abbreviation for the language that is used in the registry; for example, `fra` for French and `esn` for Spanish.

1. Update the `WBreakerClass` key value to the value from the table for the current word breaker.

1. If the selected language uses a stemmer, update the `StemmerClass` key value to the value from the table for the current stemmer.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### [Restore the previous components](#tab/restore)

1. Don't remove the files for the previous version of the components from the `Binn` folder.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID`.

1. If the following keys don't exist, use the following steps to add new keys for the COM ClassIDs for the current word breaker and stemmer interfaces for the selected language:

   1. Add a new key with the value from the table for the current word breaker.

   1. Update the (Default) data of that key value to the file name of the current word breaker from the table.

   1. If the selected language uses a stemmer, add a new key with the value from the table for the current stemmer.

   1. If the selected language uses a stemmer, update the (Default) data of that key value to the file name of the current stemmer from the table.

1. In the registry, go to the following node: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>`. `<language_key>` represents the abbreviation for the language that is used in the registry; for example, `fra` for French and `esn` for Spanish.

1. Update the `WBreakerClass` key value to the value from the table for the previous word breaker.

1. If the selected language uses a stemmer, update the `StemmerClass` key value to the value from the table for the previous stemmer.

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

---

<a id="newnewvalues"></a>

### File names and registry values for reverting and restoring word breakers and stemmers

Use the following list of file names and registry entries together with the instructions in the preceding section. Use the previous values to revert to the previous version, or use the current values to restore the current version of the components.

The following list is sorted alphabetically on the abbreviation used for each language.

#### Simplified Chinese (`chs`) - LCID 2052

| Component | Word breaker |
| --- | --- |
| Previous CLSID | `12ce94a0-defb-11d2-b31d-00600893a857` |
| Previous file name | `chsbrkr.dll` |
| Current CLSID | `e0831c90-bab0-4ca5-b9bd-ea254b538dac` |
| Current file name | `MsWb70804.dll` |

#### Traditional Chinese (`cht`) - LCID 1028

| Component | Word breaker |
| --- | --- |
| Previous CLSID | `1680e7c3-9430-4a51-9b82-1e7e7aee5258` |
| Previous file name | `chtbrkr.dll` |
| Current CLSID | `e9b1df65-08f1-438b-8277-ef462b23a792` |
| Current file name | `MsWb70404.dll` |

#### Thai (`tha`) - LCID 1054

| Component | Word breaker | Stemmer |
| --- | --- | --- |
| Previous CLSID | `cca22cf4-59fe-11d1-bbff-00c04fb97fda` | `cedc01c7-59fe-11d1-bbff-00c04fb97fda` |
| Previous file name | `Thawbrkr.dll` | `Thawbrkr.dll` |
| Current CLSID | `f70c0935-6e9f-4ef1-9f06-7876536db900` | None |
| Current file name | `MsWb7001e.dll` | None |

#### Chinese Traditional (`zh-hk`) - LCID 3076

| Component | Word breaker |
| --- | --- |
| Previous CLSID | `1680e7c3-9430-4a51-9b82-1e7e7aee5258` |
| Previous file name | `chtbrkr.dll` |
| Current CLSID | `e9b1df65-08f1-438b-8277-ef462b23a792` |
| Current file name | `MsWb70404.dll` |

#### Chinese Traditional (`zh-mo`) - LCID 5124

| Component | Word breaker |
| --- | --- |
| Previous CLSID | `1680e7c3-9430-4a51-9b82-1e7e7aee5258` |
| Previous file name | `chtbrkr.dll` |
| Current CLSID | `e9b1df65-08f1-438b-8277-ef462b23a792` |
| Current file name | `MsWb70404.dll` |

#### Chinese Simplified (`zh-sg`) - LCID 4100

| Component | Word breaker |
| --- | --- |
| Previous CLSID | `12ce94a0-defb-11d2-b31d-00600893a857` |
| Previous file name | `chsbrkr.dll` |
| Current CLSID | `e0831c90-bab0-4ca5-b9bd-ea254b538dac` |
| Current file name | `MsWb70804.dll` |

## Related content

- [Change the word breaker used for US English and UK English](change-the-word-breaker-used-for-us-english-and-uk-english.md)
- [Full-Text Search](full-text-search.md)
