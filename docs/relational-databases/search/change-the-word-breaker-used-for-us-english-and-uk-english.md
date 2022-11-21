---
description: "Change the Word Breaker Used for US English and UK English"
title: "Change the Word Breaker Used for US English and UK English | Microsoft Docs"
ms.date: "05/11/2020"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
ms.assetid: 6b5d2177-db98-47f5-b32e-4b80a2f74ffe
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change the Word Breaker Used for US English and UK English

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  Starting with [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)], setup installs an updated version of the word breaker and stemmer for the English language, replacing the previous version of these components. For information about the changed behavior of the updated components, see [Behavior Changes to Full-Text Search](/previous-versions/sql/2014/database-engine/behavior-changes-to-full-text-search#behavior-changes-in-full-text-search-in--1). This topic describes how to switch from the updated version of these components to the previous version, or to switch back from the previous version to the updated version. For cluster installations, these changes should be made on all nodes.  
  
 Some previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] used different word breakers represented by different CLSIDs for US English (LCID 1033) and UK English (LCID 2057). Starting with [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)], both LCIDs use the same components with the same CLSIDs, as shown in the following table:  
  
|LCID|Word breaker installed by previous versions<br /><br /> version 12.0.6828.0|Stemmer installed by previous versions|Word breaker installed with [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] and higher versions<br /><br /> version 14.0.4999.1038|Stemmer installed with [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)] and higher versions|  
|----------|-------------------------------------------------------------------------|--------------------------------------------|-----------------------------------------------------------------------|---------------------------------------|  
|1033<br />(US English)|188D6CC5-CB03-4C01-912E-47D21295D77E|EEED4C20-7F1B-11CE-BE57-00AA0051FE20|9FAED859-0B30-4434-AE65-412E14A16FB8|E1E5EF84-C4A6-4E50-8188-99AEF3DE2659|  
|2057<br />(UK English)|173C97E2-AEBE-437C-9445-01B237ABF2F6|D99F7670-7F1A-11CE-BE57-00AA0051FE20|9FAED859-0B30-4434-AE65-412E14A16FB8|E1E5EF84-C4A6-4E50-8188-99AEF3DE2659|  
  
 The components described in this topic are DLL files that are installed in the `MSSQL\Binn` folder for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. The full path is typically `C:\Program Files\Microsoft SQL Server\<instance>\MSSQL\Binn`.  
  
 For more information about word breakers and stemmers, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
## Switching from the current English word breaker to the previous English word breakers  
  
#### To switch from the current version of the US English word breaker to the previous version  
  
1.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\CLSID**.  
  
2.  Use the following steps to add new keyS for the COM ClassIDs for the previous US English word breaker and stemmer interfaces for LCID 1033:  
  
    1.  Add a new key with the value **{188D6CC5-CB03-4C01-912E-47D21295D77E}** for the previous word breaker.  
  
    2.  Update the (Default) data of that key value to **langwrbk.dll**.  
  
    3.  Add a new key with the value **{EEED4C20-7F1B-11CE-BE57-00AA0051FE20}** for the previous stemmer.  
  
    4.  Update the (Default) data of that key value to **infosoft.dll**.  
  
3.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\Language\enu**.  
  
4.  Update the **WBreakerClass** key value to **{188D6CC5-CB03-4C01-912E-47D21295D77E}**.  
  
5.  Update the **StemmerClass** key value to **{EEED4C20-7F1B-11CE-BE57-00AA0051FE20}**.  
  
6.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

#### To switch from the current version of the UK English word breaker to the previous version  
  
1.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\CLSID**.  
  
2.  Use the following steps to add a new key for the COM ClassIDs for the previous UK English word breaker and stemmer interfaces for LCID 2057:  
  
    1.  Add a new key with the value **{173C97E2-AEBE-437C-9445-01B237ABF2F6}** for the previous word breaker.  
  
    2.  Update the (Default) data of that key value to **langwrbk.dll**.  
  
    3.  Add a new key with the value **{D99F7670-7F1A-11CE-BE57-00AA0051FE20}** for the previous stemmer.  
  
    4.  Update the (Default) data of that key value to **infosoft.dll**.  
  
3.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\Language\eng**.  
  
4.  Update the **WBreakerClass** key value to **{173C97E2-AEBE-437C-9445-01B237ABF2F6}**.  
  
5.  Update the **StemmerClass** key value to **{D99F7670-7F1A-11CE-BE57-00AA0051FE20}**.  
  
6.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Switching back from the previous English word breakers to the current English word breaker  
  
#### To switch back from the previous version of the US English word breaker to the current version  
  
1.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\CLSID**.  
  
2.  If the following keys do not exist, then use the following steps to add a new key for the COM ClassIDs for the current US English word breaker and stemmer interfaces for LCID 1033:  
  
    1.  Add a new key with the value **{9faed859-0b30-4434-ae65-412e14a16fb8}** for the current word breaker.  
  
    2.  Update the (Default) data of that key value to **MsWb7.dll**.  
  
    3.  Add a new key with the value **{e1e5ef84-c4a6-4e50-8188-99aef3de2659}** for the current stemmer.  
  
    4.  Update the (Default) data of that key value to **MsWb7.dll**.  
  
3.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\Language\eng**.  
  
4.  Update the **WBreakerClass** key value to **{9faed859-0b30-4434-ae65-412e14a16fb8}**.  
  
5.  Update the **StemmerClass** key value to **{e1e5ef84-c4a6-4e50-8188-99aef3de2659}**.  
  
6.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
#### To switch back from the previous version of the UK English word breaker to the current version  
  
1.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\CLSID**.  
  
2.  If the following keys do not exist, then use the following steps to add a new key for the COM ClassIDs for the current UK English word breaker and stemmer interfaces for LCID 2057:  
  
    1.  Add a new key with the value **{9faed859-0b30-4434-ae65-412e14a16fb8}** for the current word breaker.  
  
    2.  Update the (Default) data of that key value to **MsWb7.dll**.  
  
    3.  Add a new key with the value **{e1e5ef84-c4a6-4e50-8188-99aef3de2659}** for the current stemmer.  
  
    4.  Update the (Default) data of that key value to **MsWb7.dll**.  
  
3.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<InstanceRoot\>\MSSearch\Language\eng**.  
  
4.  Update the **WBreakerClass** key value to **{9faed859-0b30-4434-ae65-412e14a16fb8}**.  
  
5.  Update the **StemmerClass** key value to **{e1e5ef84-c4a6-4e50-8188-99aef3de2659}**.  
  
6.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See Also  
 [Revert the Word Breakers Used by Search to the Previous Version](../../relational-databases/search/revert-the-word-breakers-used-by-search-to-the-previous-version.md)   
 [Behavior Changes to Full-Text Search](./full-text-search.md)  
