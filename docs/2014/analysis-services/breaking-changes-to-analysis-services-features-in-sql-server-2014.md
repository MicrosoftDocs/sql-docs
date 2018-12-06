---
title: "Breaking Changes to Analysis Services Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "breaking changes [Analysis Services]"
  - "upgrading Analysis Services"
ms.assetid: aeb02542-5a6c-458c-a110-13413dd3e9d9
author: minewiskan
ms.author: owend
manager: craigg
---
# Breaking Changes to Analysis Services Features in SQL Server 2014
  This topic describes breaking changes in [!INCLUDE[ssASCurrent](../includes/ssascurrent-md.md)]. These changes might break applications, scripts, or functionality based on earlier versions of SQL Server.  
  
 In this topic:  
  
-   [Breaking Changes in SQL Server 2014](#bkmk_sql2014)  
  
-   [Breaking Changes in SQL Server 2012 SP1](#bkmk_2012Sp1)  
  
-   [Breaking Changes in SQL Server 2012](#bkmk_sql11)  
  
-   [Breaking Changes in SQL Server 2008/SQL Server 2008 R2](#bkmk_sql10)  
  
##  <a name="bkmk_sql2014"></a> Breaking Changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 There are no new breaking changes announced for tabular, multidimensional, data mining, or [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] features in this release.  However, because  [!INCLUDE[ssASCurrent](../includes/ssascurrent-md.md)] is so similar to the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] versions, breaking changes from both prior releases are provided here as a convenience in case you're upgrading from [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
##  <a name="bkmk_2012Sp1"></a> Breaking Changes in SQL Server 2012 SP1  
 Globalization-related code changes have been known to break some applications. Known issues include:  
  
 **Case-sensitivity of object identifiers**  
 A code change intended to make all object identifiers case-insensitive is having the opposite effect for some languages. The intention is that all object identifiers will be case-insensitive, regardless of collation. This change aligns Analysis Services with other applications typically used in the same solution stack.  
  
 For languages based on the 26 characters of the basic Latin alphabet, object identifiers are now case insensitive, which is the intended behavior.  
  
 For Cyrillic and other bicameral language scripts that use casing (Greek, Armenian, and Coptic), object identifiers are now case-sensitive. Breaking changes are most likely to occur when there is case difference between an object identifier and how it is referenced (for example, a processing script that refers to the object identifier in all lower-case). This behavior is likely to change in the future, but as a temporary workaround, we suggest modifying scripts to use the same case as the object identifier.  
  
##  <a name="bkmk_sql11"></a> Breaking Changes in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
 This section documents the breaking changes reported for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
|Issue|Description|  
|-----------|-----------------|  
|Setup commands removed for a [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] installation.|Setup installs, but no longer configures, a [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)]. Setup commands that collected values used for configuration actions are now removed. These include /FARMACCOUNT, /FARMPASSWORD, /PASSPHRASE, and /FARMADMINPORT.<br /><br /> If you created installation scripts for unattended setup, you will need to modify those scripts for a [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] installation. The alternative is to use PowerShell cmdlets to configure the server in unattended mode. For more information, see [Install PowerPivot from the Command Prompt](../../2014/sql-server/install/install-powerpivot-from-the-command-prompt.md) and [PowerPivot Configuration using Windows PowerShell](power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md).|  
  
##  <a name="bkmk_sql10"></a> Breaking Changes in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]/[!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)]  
 This section contains the breaking changes from previous releases. If you are upgrading from [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], you should review the breaking changes that were introduced in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)].  
  
|Issue|Description|  
|-----------|-----------------|  
|The shallow exists function now works differently with named sets that contain enumerated members or crossjoins of enumsets.|In [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)], the shallow exists function did not work with named sets that contained enumerated members or crossjoins of enumsets. For backward compatibility with the original release version and SP1 of [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)], set the configuration property "ConfigurationSettings\OLAP\Query\NamedSetShallowExistsMode" to 1, or for backward compatibility with [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)] SP2, set it to 2.|  
|VBA functions handle null values and empty values differently than they were handled in [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)]|In [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)], VBA functions returned 0 or an empty string when either null values or empty values were used as arguments. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], they will return null.|  
|The Migration Wizard will fail because DSO is not installed by Default.|By default, SQL Server 2008 does not install the DSO (Decision Support Objects) backward compatibility component. The backward compatibility package is installed by default but the DSO component of the package will be disabled. Since the SQL Server Analysis Services Migration Wizard relies on this component, it will fail unless the component is installed. To install the DSO component, do the following:<br /><br /> 1) Open Control Panel.<br />2) In Windows XP or Windows Server 2003, select **Add or Remove Programs**. In Windows Vista and Windows Server 2008, select **Programs and Features**.<br />3) Right-click **Microsoft SQL Server 2005 Backward Compatibility**, and select **Change**.<br />4) In the Backward Compatibility Setup wizard, click **Next**.<br />5) On the Program Maintenance page, select **Modify**, and then click **Next**.<br />6) On the Feature Selection page, if Decision Support Objects (DSO) is not available, click the down arrow and select **This feature will be installed on local hard drive**. Click **Next**.<br />7) On the Ready to Modify the Program page, click **Install**.<br />8) When installation is finished, click **Finish**.<br /><br /> <br /><br /> You can remove DSO after migration is complete by following the previous steps, changing the option for DSO to "**This feature will not be available**."<br /><br /> If the backward compatibility package is not installed, you can install it from the SQL Server 2008 distribution media. Note that there are versions for each target architecture (x86, x64, ia64). These versions can be found at the following locations:<br /><br /> x86\Setup\x86\SQLServer2005_BC.msi<br /><br /> x64\Setup\x64\SQLServer2005_BC.msi<br /><br /> ia64\Setup\ia64\SQLServer2005_BC.msi|  
|It is not recommended to put the partition location in the Data folder.|The server manages the Data folder and creates or drops folders as objects are created, deleted, and altered. Therefore, specifying a partition storage location inside the Data folder is strongly discouraged, especially in the subfolders for databases, cubes, and dimensions. Although the server allows you to do this with Create or Alter, it will display a warning. When you upgrade databases from SQL Server 2005 Analysis Services to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] Analysis Services that have partition storage locations in the Data folder, it will work. Restore or Sync will require that you move partition storage locations outside the Data folder.|  
|You might get unexpected results for queries that use the "EXISTING" MDX keyword in ProClarity Analytics Server and Microsoft Office PerformancePoint Server 2007.|ProClarity Analytics Server and Microsoft Office PerformancePoint Server 2007 use the EXISTING keyword in MDX incorrectly in certain scenarios. Due to changes made in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] Analysis Services, these queries might return unexpected results.|  
  
## See Also  
 [Analysis Services Backward Compatibility](analysis-services-backward-compatibility.md)  
  
  
