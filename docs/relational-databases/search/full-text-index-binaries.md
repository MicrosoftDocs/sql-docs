---
title: Filters and Word Breakers
titleSuffix: SQL Server Full-Text Search
description: View list of SQL Server Full-Text Search filters and word breakers.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 03/06/2026
ms.service: sql
ms.subservice: search
ms.topic: concept-article
helpviewer_keywords:
  - "full-text search [SQL Server], word breakers"
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
  - "word breakers [full-text search]"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Filters and word breakers - SQL Server Full-Text Search

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] installs new word breakers and filters, replacing all previous versions of these components.

- **Version 2** - Binaries installed with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
- **Version 1** - Binaries installed with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.

To keep using version 1 binaries after an in-place upgrade to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]:

- Copy the filter, word breaker, and other dependent binaries from the previous [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance's `C:\Program Files\Microsoft SQL Server\MSSQL16.<instance-name>\MSSQL\Binn` directory to the corresponding `C:\Program Files\Microsoft SQL Server\MSSQL17.<instance-name>\MSSQL\Binn` folder.
- The upgrade preserves the associated registry settings and doesn't remove them in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

## Filters

### [Version 1](#tab/version1)

| Filter | Extension | CLSID |
| --- | --- | --- |
| `msfte.dll` | `.asm`, `.bat`, `.c`, `.cmd`, `.cpp`, `.cxx`, `.def`, `.dic`, `.h`, `.hpp`, `.hxx`, `.ibq`, `.idl`, `.inc`, `.inf`, `.ini`, `.inx`, `.js`, `.log`, `.m3u`, `.pl`, `.rc`, `.reg`, `.rtf`, `.txt`, `.url`, `.vbs`, `.wtx` | `C7310720-AC80-11D1-8DF3-00C04FB6EF4F` |
| `nlhtml.dll` | `.ascx`, `.asp`, `.aspx`, `.hhc`, `.htm`, `.html`, `.htw`, `.htx`, `.odc`, `.stm` | `E0CA5340-4534-11CF-B952-00AA0051FE20` |
| `xmltfilt.dll` | `.xml` | `41B9BE05-B3AF-460C-BF0B-2CDD44A093B1` |

### [Version 2](#tab/version2)

| Filter | Extension | CLSID |
| --- | --- | --- |
| `nlhtml02.dll` | `.ascx`, `.asp`, `.aspx`, `.css`, `.hhc`, `.hta`, `.htm`, `.html`, `.htt`, `.htw`, `.htx`, `.odc`, `.shtm`, `.shtml`, `.sor`, `.srf`, `.stm` | `56BD18AD-CF9C-4110-AAAA-B2F96887D123` |
| `offfilt02.dll` | `.doc`, `.dot`, `.obd`, `.obt`, `.pot`, `.pps`, `.ppt`, `.xlb`, `.xlc`, `.xls`, `.xlt` | `64F1276A-7A68-4190-882C-5F14B7852019` |
| `offfiltx02.dll` | `.docm`, `.docx`, `.dotx`<br /><br />`.pptm`, `.pptx`<br /><br />`.xlsm`, `.xlsx`<br /><br />`.xlsb`<br /><br />`.zip` | `5A98B233-3C59-4B31-944C-0E560D85E6C3`<br /><br />`DDFE337F-4987-4EC8-BDE3-133FA63D5D85`<br /><br />`F90DFE0C-CBDF-41FF-8598-EDD8F222A2C8`<br /><br />`312AB530-ECC9-496E-AE0E-C9E6C5392499`<br /><br />`20E823C2-62F3-4638-96BD-90F4F6784EBC` |
| `odffilt02.dll` | `.odt`<br /><br />`.odp`<br /><br />`.ods` | `9FBC2D8F-6F52-4CFA-A86F-096F3E9EB4B2`<br /><br />`4693FF15-B962-420A-9E5D-176F7D4B8321`<br /><br />`E2F5480E-ED5A-4DDE-B8A8-F9F297479F62` |
| `onfilter02.dll` | `.one` | `B8D12492-CE0F-40AD-83EA-099A03D493F1` |
| `msgfilt02.dll` | `.msg` | `4039B326-9F27-4B4A-B460-47A0C6A39D5C` |

---

## Word breakers

### [Version 1](#tab/version1)

| Language | LCID | Word breaker | Other dependent files | CLSID |
| --- | --- | --- | --- | --- |
| Neutral | 0 | `NaturalLanguage6.dll` | `NlsData0000.dll` | `1D49F57D-47D2-4AEE-A69B-593EC558773F` |
| Arabic | 1025 | `MsWb7.dll` | `Prm0001.bin` | `04B37E30-C9A9-4A7D-8F20-792FC87DDF71` |
| Bulgarian | 1026 | `NaturalLanguage6.dll` | `NlsData0002.dll`<br />`NlsLexicons0002.dll` | `B675B948-FBA8-46A4-A4C7-D4291785127B` |
| Catalan | 1027 | `NaturalLanguage6.dll` | `NlsData0003.dll`<br />`NlsLexicons0003.dll` | `3D0B8752-68F8-4F39-929D-DE20ED323F45` |
| Traditional Chinese | 1028 | `MsWb70404.dll` | `NL7Data0404.dll`<br />`NL7Lexicons0404.dll`<br />`NL7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` |
| Czech | 1029 | `MsWb7.dll` | `Prm0005.bin` | `468BFC77-3876-4A47-A6FF-F5F6E8EA7968` |
| Danish | 1030 | `MsWb7.dll` | `Prm0006.bin` | `4645F4F2-6359-4D81-BFB5-DAFBF89812B4` |
| German | 1031 | `MsWb7.dll` | `Prm0007.bin` | `DFA00C33-BF19-482E-A791-3C785B0149B4` |
| Greek | 1032 | `MsWb7.dll` | `Prm0008.bin` | `0F7679E0-1386-493C-AF00-DAC67B1B16B1` |
| English | 1033 | `MsWb7.dll` | `Prm0009.bin` | `9FAED859-0B30-4434-AE65-412E14A16FB8` |
| French | 1036 | `NaturalLanguage6.dll` | `NlsData000c.dll`<br />`NlsLexicons000c.dll` | `92F2118A-E813-4A4D-9DE2-F96A9DC02C53` |
| Hebrew | 1037 | `NaturalLanguage6.dll` | `NlsData000d.dll`<br />`NlsLexicons000d.dll` | `E5B2CB7A-FD35-4D4B-A147-176FEB42244B` |
| Icelandic | 1039 | `NaturalLanguage6.dll` | `NlsData000f.dll`<br />`NlsLexicons000f.dll` | `53DA1CBB-0F45-46A4-AA6E-47CAAD84C921` |
| Italian | 1040 | `NaturalLanguage6.dll` | `NlsData0010.dll`<br />`NlsLexicons0010.dll` | `7E352021-69D6-4553-86AC-430B0D8FF913` |
| Japanese | 1041 | `MsWb70011.dll` | `NL7Data0011.dll`<br />`NL7Lexicons0011.dll`<br />`NL7Models0011.dll` | `04096682-6ECE-4E9E-90C1-52D81F0422ED` |
| Korean | 1042 | `korwbrkr.dll` | `korwbrkr.lex` | `737FC9B5-BC66-4953-B7DB-B79A72A592A5` |
| Dutch | 1043 | `MsWb7.dll` | `Prm0013.bin` | `69483C30-A9AF-4552-8F84-A0796AD5285B` |
| Bokmål | 1044 | `NaturalLanguage6.dll` | `NlsData0414.dll`<br />`NlsLexicons0414.dll` | `A9C6B8DD-3CBB-44CB-AA44-4B1C0DBB404D` |
| Polish | 1045 | `MsWb7.dll` | `Prm0015.bin` | `0D5EBEA3-B982-46B9-9378-4C238262C12C` |
| Brazilian | 1046 | `NaturalLanguage6.dll` | `NlsData0416.dll`<br />`NlsLexicons0416.dll` | `A25A5CCD-80F4-4E02-AADD-7F39CC55E737` |
| Romanian | 1048 | `NaturalLanguage6.dll` | `NlsData0018.dll`<br />`NlsLexicons0018.dll` | `D0458F37-2228-4FC7-9E66-34133DF4C929` |
| Russian | 1049 | `MsWb7.dll` | `Prm0019.bin` | `AAA3D3BD-6DE7-4317-91A0-D25E7D3BABC3` |
| Croatian | 1050 | `NaturalLanguage6.dll` | `NlsData001a.dll`<br />`NlsLexicons001a.dll` | `712720F4-F4FF-46CF-B6EC-2CC24FC873A5` |
| Slovak | 1051 | `NaturalLanguage6.dll` | `NlsData001b.dll`<br />`NlsLexicons001b.dll` | `2652B813-2260-4EF3-A311-74A7AC6513D7` |
| Swedish | 1053 | `NaturalLanguage6.dll` | `NlsData001d.dll`<br />`NlsLexicons001d.dll` | `2CB861BB-B1B4-4E14-A1A7-D3FB30C3F5CF` |
| Thai | 1054 | `MsWb7001e.dll` | `NL7Data001e.dll`<br />`NL7Lexicons001e.dll`<br />`NL7Models001e.dll` | `F70C0935-6E9F-4EF1-9F06-7876536DB900` |
| Turkish | 1055 | `MsWb7.dll` | `Prm001f.bin` | `54A12380-E78E-4980-BD96-2EF7076B5BB0` |
| Urdu | 1056 | `NaturalLanguage6.dll` | `NlsData0020.dll`<br />`NlsLexicons0020.dll` | `F3AEB884-58C8-40CF-AED3-E7EEFFFAA04A` |
| Indonesian | 1057 | `NaturalLanguage6.dll` | `NlsData0021.dll`<br />`NlsLexicons0021.dll` | `F7B02D8A-65DB-41CB-894D-5BBBF96C1B42` |
| Ukrainian | 1058 | `NaturalLanguage6.dll` | `NlsData0022.dll`<br />`NlsLexicons0022.dll` | `773229CD-D53C-4211-ACD8-8F2C7BF2AE7C` |
| Slovenian | 1060 | `NaturalLanguage6.dll` | `NlsData0024.dll`<br />`NlsLexicons0024.dll` | `8A899610-150A-40DB-B57A-940EDB3203CE` |
| Latvian | 1062 | `NaturalLanguage6.dll` | `NlsData0026.dll`<br />`NlsLexicons0026.dll` | `C700F6EF-A80F-4B24-922A-32308B6FF0C3` |
| Lithuanian | 1063 | `NaturalLanguage6.dll` | `NlsData0027.dll`<br />`NlsLexicons0027.dll` | `1C0D39B2-C788-40D2-B062-FDF8293D7BC6` |
| Vietnamese | 1066 | `NaturalLanguage6.dll` | `NlsData002a.dll`<br />`NlsLexicons002a.dll` | `70878DCD-56F6-4681-BC52-BC7F58EDF723` |
| Hindi | 1081 | `NaturalLanguage6.dll` | `NlsData0039.dll`<br />`NlsLexicons0039.dll` | `0F0549A6-C2E0-442A-85D7-20E3DB9B6A1F` |
| Malay - Malaysia | 1086 | `NaturalLanguage6.dll` | `NlsData003e.dll`<br />`NlsLexicons003e.dll` | `EB6C9433-4AAB-4B71-8B18-8F7A3812E43A` |
| Bengali (India) | 1093 | `NaturalLanguage6.dll` | `NlsData0045.dll`<br />`NlsLexicons0045.dll` | `05C9DA2B-6DFF-42C7-81CC-706DAE08BC7A` |
| Punjabi | 1094 | `NaturalLanguage6.dll` | `NlsData0046.dll`<br />`NlsLexicons0021.dll` | `9FE6E853-B35F-4FE4-B006-33148455093E` |
| Gujarati | 1095 | `NaturalLanguage6.dll` | `NlsData0047.dll`<br />`NlsLexicons0047.dll` | `04DA8451-7F63-4870-A4D7-F55BE66BFDFB` |
| Tamil | 1097 | `NaturalLanguage6.dll` | `NlsData0049.dll`<br />`NlsLexicons0049.dll` | `6C53A912-47C6-4959-B342-DF6C9DA9D494` |
| Telugu | 1098 | `NaturalLanguage6.dll` | `NlsData004a.dll`<br />`NlsLexicons004a.dll` | `136E0057-D7ED-4B85-9F62-1318CFE1573B` |
| Kannada | 1099 | `NaturalLanguage6.dll` | `NlsData0004.dll`<br />`NlsLexicons0004.dll` | `4495524E-2E54-472D-86D7-D671CA588F01` |
| Malayalam | 1100 | `NaturalLanguage6.dll` | `NlsData004c.dll`<br />`NlsLexicons004c.dll` | `69ED626B-904D-4DEF-B919-9EF7E4E339DD` |
| Marathi | 1102 | `NaturalLanguage6.dll` | `NlsData004e.dll`<br />`NlsLexicons004e.dll` | `8B3302D7-95F6-4BC5-A06A-0D6DEF15DB69` |
| Simplified Chinese | 2052 | `MsWb70804.dll` | `NL7Data0804.dll`<br />`NL7Lexicons0804.dll`<br />`NL7Models0804.dll` | `E0831C90-BAB0-4CA5-B9BD-EA254B538DAC` |
| British English | 2057 | `MsWb7.dll` | `Prm0009.bin` | `9FAED859-0B30-4434-AE65-412E14A16FB8` |
| Portuguese | 2070 | `NaturalLanguage6.dll` | `NlsData0816.dll`<br />`NlsLexicons0816.dll` | `C4BF21DA-F1E5-4C7F-A611-2698645B19EF` |
| Serbian (Latin) | 2074 | `NaturalLanguage6.dll` | `NlsData081a.dll`<br />`NlsLexicons081a.dll` | `EE38A9FC-437F-4D03-A593-BB92AF0D153C` |
| Chinese (Hong Kong SAR, PRC) | 3076 | `MsWb70404.dll` | `NL7Data0404.dll`<br />`NL7Lexicons0404.dll`<br />`NL7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` |
| Spanish | 3082 | `NaturalLanguage6.dll` | `NlsData000a.dll`<br />`NlsLexicons000a.dll` | `68DC71DC-2327-4040-8F03-50D6A9805049` |
| Serbian (Cyrillic) | 3098 | `NaturalLanguage6.dll` | `NlsData0c1a.dll`<br />`NlsLexicons0c1a.dll` | `C28DA8E5-39C2-4F62-82FA-C61D39A196DF` |
| Chinese (Singapore) | 4100 | `MsWb70804.dll` | `NL7Data0804.dll`<br />`NL7Lexicons0804.dll`<br />`NL7Models0804.dll` | `E0831C90-BAB0-4CA5-B9BD-EA254B538DAC` |
| Chinese (Macao SAR) | 5124 | `MsWb70404.dll` | `NL7Data0404.dll`<br />`NL7Lexicons0404.dll`<br />`NL7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` |

### [Version 2](#tab/version2)

| Language | LCID | Word breaker DLL | Other dependent files | Word breaker CLSID | Stemmer CLSID |
| --- | --- | --- | --- | --- | --- |
| Neutral | 0 | `MSWB7.dll` | `prm0009.bin` | `9FAED859-0B30-4434-AE65-412E14A16FB8` | `E1E5EF84-C4A6-4E50-8188-99AEF3DE2659` |
| Arabic | 1025 | `CMICArabicWordBreaker.dll` | `FastMorph.dll` | `04CC0DAE-B6F8-4086-9B92-635F41B538C0` | N/A |
| Bulgarian | 1026 | `MSWB7.dll` | `prm0002.bin` | `A7F66238-8693-4016-B146-650E93B8797E` | `32C05E09-E77A-4E79-90B6-606F89A26C73` |
| Catalan | 1027 | `MSWB7.dll` | `prm0003.bin` | `75EADC14-5867-45A8-8CE0-DE646F7BFE51` | `D850842F-F246-4322-A1E4-7C983EAF8011` |
| Traditional Chinese | 1028 | `MsWb70404.dll` | `Nl7Lexicons0404.dll`<br />`Nl7Data0404.dll`<br />`Nl7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` | N/A |
| Czech | 1029 | `MSWB7.dll` | `prm0005.bin` | `468BFC77-3876-4A47-A6FF-F5F6E8EA7968` | `F51B7203-9BF9-4C39-B655-18FAD8FA8A9A` |
| Danish | 1030 | `MSWB7.dll` | `prm0006.bin` | `4645F4F2-6359-4D81-BFB5-DAFBF89812B4` | `950E4995-301B-4613-8042-F041BC34F32B` |
| German | 1031 | `MSWB7.dll` | `prm0007.bin` | `DFA00C33-BF19-482E-A791-3C785B0149B4` | `8A474D89-6E2F-419C-8DD5-9B50EDC8C787` |
| Greek | 1032 | `MSWB7.dll` | `prm0008.bin` | `0F7679E0-1386-493C-AF00-DAC67B1B16B1` | `26D868ED-CA80-481B-B138-3E8C661161B1` |
| English | 1033 | `MSWB7.dll` | `prm0009.bin` | `9FAED859-0B30-4434-AE65-412E14A16FB8` | `E1E5EF84-C4A6-4E50-8188-99AEF3DE2659` |
| Finnish | 1035 | `MSWB7.dll` | `prm000b.bin` | `08F2F5A6-65FF-44DD-8000-E9343F8FBC07` | N/A |
| French | 1036 | `MSWB7.dll` | `prm000c.bin` | `97C2E40B-F712-4084-8DFB-1806C8A87037` | `A6F6669F-9AAD-4060-9648-CBF2CFF0DF7A` |
| Hebrew | 1037 | `MSWB7.dll` | `prm000d.bin` | `F40E93A1-9670-48C6-B814-1B0566A19AB5` | N/A |
| Hungarian | 1038 | `MSWB7.dll` | `prm000e.bin` | `E1539A26-C204-405A-9AFD-A516B849F91B` | N/A |
| Icelandic | 1039 | `MSWB7.dll` | `prm000f.bin` | `78475969-900B-4CC2-9746-B9C3B9E45FC2` | `C0C08E6F-F174-489D-9DCC-044C6FCBD3FC` |
| Italian | 1040 | `MSWB7.dll` | `prm0010.bin` | `F1589D2A-D2A3-4C27-AE23-EF5686FFE4F8` | `3315DB6D-4D55-4EF2-8288-EA0EFD43901F` |
| Japanese | 1041 | `MsWb70011_v2.dll` | `Nl7Data0011_v2.dll`<br />`Nl7Lexicons0011_v2.dll`<br />`Nl7Models0011_v2.dll` | `04096682-6ECE-4E9E-90C1-52D81F0422ED` | N/A |
| Korean | 1042 | `korwbrkr.dll` | `korwbrkr.lex`<br />`ko.complex.rule.bin`<br />`ko.token.rule.bin`<br />`korwbconfig.ini` | `737FC9B5-BC66-4953-B7DB-B79A72A592A5` | N/A |
| Dutch | 1043 | `MSWB7.dll` | `prm0013.bin` | `69483C30-A9AF-4552-8F84-A0796AD5285B` | `7C83A911-F24B-45F7-92B8-348484C33BA5` |
| Bokmål | 1044 | `MSWB7.dll` | `prm0414.bin` | `2259C480-D589-47E0-94D2-F2E532D24FCC` | `D8346670-A0A9-414B-9E5C-382711774B55` |
| Polish | 1045 | `MSWB7.dll` | `prm0015.bin` | `0D5EBEA3-B982-46B9-9378-4C238262C12C` | `6D2D2C1D-5A3C-43C2-96D9-F3878D0A1B69` |
| Brazilian | 1046 | `MSWB7.dll` | `prm0416.bin` | `77B475A5-3793-4E7F-B809-00420FDA7E8B` | `D8C06C28-6F61-4453-AD10-2594DE7F5DB0` |
| Romanian | 1048 | `MSWB7.dll` | `prm0018.bin` | `B3B2ED45-B065-4A63-9EE3-2FBAA7C0229B` | `133CF1CF-EC14-41C6-91BD-13300A1D3E28` |
| Russian | 1049 | `MSWB7.dll` | `prm0019.bin` | `AAA3D3BD-6DE7-4317-91A0-D25E7D3BABC3` | `D42C8B70-ADEB-4B81-A52F-C09F24F77DFA` |
| Croatian | 1050 | `MSWB7.dll` | `prm001a.bin` | `6C59738A-8ED1-4D39-B1CF-D5DC09A5A7EC` | `17F84C90-E508-4E1F-B24C-2EFF850EEBB6` |
| Slovak | 1051 | `MSWB7.dll` | `prm001b.bin` | `5C820763-3D0A-4C2A-88D8-0C2F9AE37C2C` | N/A |
| Swedish | 1053 | `MSWB7.dll` | `prm001d.bin` | `2799D0DD-0AB5-4E6D-B9DD-F2787225DE8D` | `1F58479B-961A-4F6C-AD8B-F7DCF87EFE5B` |
| Thai | 1054 | `MsWb7001E.dll` | `Nl7Data001e.dll`<br />`Nl7Lexicons001e.dll`<br />`Nl7Models001e.dll` | `F70C0935-6E9F-4EF1-9F06-7876536DB900` | N/A |
| Turkish | 1055 | `MSWB7.dll` | `prm001f.bin` | `54A12380-E78E-4980-BD96-2EF7076B5BB0` | N/A |
| Urdu | 1056 | `MSWB7.dll` | `prm0020.bin` | `933788FC-63CD-40B2-B3D1-C7503EB1E2DD` | `997A7370-0E41-48D1-99C3-21C7C351A257` |
| Indonesian | 1057 | `MSWB7.dll` | `prm0021.bin` | `5E0C37FB-D90F-48EE-89F2-FC9D2E6D330C` | `B6477447-B373-47CE-9FBA-63FBABA5505A` |
| Ukrainian | 1058 | `MSWB7.dll` | `prm0022.bin` | `F7D519CD-2D4B-40F6-B2FC-67FE005FE83A` | `38FC817B-576B-4DDC-AA72-9E8D5424A16D` |
| Slovenian | 1060 | `MSWB7.dll` | `prm0024.bin` | `FB1374D7-07B3-4C77-A65A-F5FAC04B87E7` | N/A |
| Estonian | 1061 | `MSWB7.dll` | `prm0025.bin` | `DD14498E-8503-46DC-BB94-C35FBE98E60C` | N/A |
| Latvian | 1062 | `MSWB7.dll` | `prm0026.bin` | `FB37B80E-CE48-40F1-8440-D76748C44DB7` | N/A |
| Lithuanian | 1063 | `MSWB7.dll` | `prm0027.bin` | `7130526B-A1CE-4221-B8DC-6413E92A772C` | N/A |
| Vietnamese | 1066 | `MSWB7.dll` | `prm002a.bin` | `F5E89069-8A90-4B80-AB15-E92AA29B75EE` | N/A |
| Hindi | 1081 | `MSWB7.dll` | `prm0039.bin` | `AA5BEE8C-8C4F-458A-9ECC-95BC16C74AA6` | `E3EA5027-9343-4884-B353-4D2644663E2E` |
| Malay - Malaysia | 1086 | `MSWB7.dll` | `prm003e.bin` | `3A7D6BB6-F0F9-4B54-855B-2012989E098E` | `D32A0939-D435-4189-89FF-664A4E187031` |
| Bengali (India)<br />Bangla | 1093<br />2117 | `MSWB7.dll` | `prm0045.bin` | `1D9B4138-3171-4A01-AA2A-BB935E929280` | `8DD561A8-7C51-4BDC-AD76-1DBEEA79BFCD` |
| Punjabi | 1094 | `MSWB7.dll` | `prm0046.bin` | `8E868F6F-D2C3-416A-B2A4-2993067E4252` | `F7505172-8E37-4678-A7C5-D181A4D20B68` |
| Gujarati | 1095 | `MSWB7.dll` | `prm0047.bin` | `57CCB731-789E-4C65-8CEA-E0CDC399A0E1` | `9EB99DC4-5915-4D5A-AD6B-3A7A7A0FA796` |
| Tamil | 1097 | `MSWB7.dll` | `prm0049.bin` | `2F1EA773-6ADB-4E52-AD2E-13C3B04CCDFC` | `5FABC9F0-ACAA-4F5B-AC9B-8B8166AD1206` |
| Telugu | 1098 | `MSWB7.dll` | `prm004a.bin` | `9630D05D-4F9A-44E4-A7B5-20F7C8C04C99` | `EF1E811D-DFF3-46F8-B394-6B5075FE14D9` |
| Kannada | 1099 | `MSWB7.dll` | `prm004b.bin` | `58974A06-30BB-4800-9715-B3B12A6E2E43` | `0F2DA2B3-B4CE-412B-A74B-7EC746B4307F` |
| Malayalam | 1100 | `MSWB7.dll` | `prm004c.bin` | `FD6A88D5-6E25-4380-AC2D-23F0C40C854B` | `2CB1F188-B116-43D1-B9CD-92E746997301` |
| Marathi | 1102 | `MSWB7.dll` | `prm004e.bin` | `95354B74-5A61-4811-8BE7-FAC674C7476D` | `AF8C77A1-284B-4477-AC82-11FF39A60035` |
| Simplified Chinese | 2052 | `MsWb70804.dll` | `Nl7Lexicons0804.dll`<br />`Nl7Data0804.dll`<br />`Nl7Models0804.dll` | `E0831C90-BAB0-4CA5-B9BD-EA254B538DAC` | N/A |
| British English | 2057 | `MSWB7.dll` | `prm0009.bin` | `9FAED859-0B30-4434-AE65-412E14A16FB8` | `E1E5EF84-C4A6-4E50-8188-99AEF3DE2659` |
| Norwegian | 2068 | `MSWB7.dll` | `prm0414.bin` | `2259C480-D589-47E0-94D2-F2E532D24FCC` | `D8346670-A0A9-414B-9E5C-382711774B55` |
| Portuguese | 2070 | `MSWB7.dll` | `prm0816.bin` | `77B475A5-3793-4E7F-B809-00420FDA7E8B` | `D8C06C28-6F61-4453-AD10-2594DE7F5DB0` |
| Serbian (Latin)<br />Serbian (Sr-Latin) | 2074<br />9242 | `MSWB7.dll` | `prm281a.bin`<br />`prm241a.bin` | `EC677E4C-41FE-4FF9-8CA3-A449E63745BE` | `28B0BC0C-B0D6-42C4-8278-485ED26AA70B` |
| Chinese (Hong Kong SAR) | 3076 | `MsWb70404.dll` | `Nl7Lexicons0404.dll`<br />`Nl7Data0404.dll`<br />`Nl7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` | N/A |
| Spanish | 3082 | `MSWB7.dll` | `prm000a.bin` | `0914EA43-45A5-4D15-834F-10199CDF8793` | `7EF7EDDF-6BEA-4D5C-99FB-300FA9F11215` |
| Serbian (Cyrillic) | 3098 | `MSWB7.dll` | `prm281a.bin`<br />`prm241a.bin` | `EC677E4C-41FE-4FF9-8CA3-A449E63745BE` | `28B0BC0C-B0D6-42C4-8278-485ED26AA70B` |
| Chinese (Singapore) | 4100 | `MsWb70804.dll` | `Nl7Lexicons0804.dll`<br />`Nl7Data0804.dll`<br />`Nl7Models0804.dll` | `E0831C90-BAB0-4CA5-B9BD-EA254B538DAC` | N/A |
| Chinese (Macao SAR) | 5124 | `MsWb70404.dll` | `Nl7Lexicons0404.dll`<br />`Nl7Data0404.dll`<br />`Nl7Models0404.dll` | `E9B1DF65-08F1-438B-8277-EF462B23A792` | N/A |

---

## Related content

- [View or change registered filters and word breakers (SQL Server Search)](view-or-change-registered-filters-and-word-breakers.md)
- [Configure and Manage Filters for Search](configure-and-manage-filters-for-search.md)
- [Configure and manage word breakers and stemmers for search (SQL Server)](configure-and-manage-word-breakers-and-stemmers-for-search.md)
