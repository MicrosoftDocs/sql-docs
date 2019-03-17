---
title: "Change the Word Breaker Used for US English and UK English | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
ms.assetid: 6b5d2177-db98-47f5-b32e-4b80a2f74ffe
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change the Word Breaker Used for US English and UK English
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installs a new version (version 14.0.4999.1038) of the word breaker and stemmer for the English language, replacing the previous version of these components (version 12.0.6828.0). For information about the changed behavior of the new components, see [Behavior Changes to Full-Text Search](https://msdn.microsoft.com/library/573444e8-51bc-4f3d-9813-0037d2e13b8f). This topic describes how to switch from the new version of these components to the previous version, or to switch back from the previous version to the new version. For cluster installations, these changes should be made on all the primary and passive nodes.  
  
 Previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] used different word breakers represented by different CLSIDs for US English (LCID 1033) and UK English (LCID 2057). In this release, both LCIDs use the same components with the same CLSIDs, as shown in the following table:  
  
|LCID|Word breaker installed by previous versions<br /><br /> version 12.0.6828.0|Stemmer installed by previous versions|Word breaker installed by this version<br /><br /> version 14.0.4999.1038|Stemmer installed by this version|  
|----------|-------------------------------------------------------------------------|--------------------------------------------|-----------------------------------------------------------------------|---------------------------------------|  
|1033<br />(US English)|188D6CC5-CB03-4C01-912E-47D21295D77E|EEED4C20-7F1B-11CE-BE57-00AA0051FE20|9faed859-0b30-4434-ae65-412e14a16fb8|e1e5ef84-c4a6-4e50-8188-99aef3de2659|  
|2057<br />(UK English)|173C97E2-AEBE-437C-9445-01B237ABF2F6|D99F7670-7F1A-11CE-BE57-00AA0051FE20|9faed859-0b30-4434-ae65-412e14a16fb8|e1e5ef84-c4a6-4e50-8188-99aef3de2659|  
  
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
 [Behavior Changes to Full-Text Search](https://msdn.microsoft.com/library/573444e8-51bc-4f3d-9813-0037d2e13b8f)  
  
  
