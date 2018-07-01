---
title: "Analysis Services PowerShell Reference | Microsoft Docs"
ms.date: 06/25/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: powershell
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Analysis Services PowerShell Reference
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] PowerShell cmdlets are included in the [SqlServer module](https://www.powershellgallery.com/packages/SqlServer/21.0.17099). 
  
>[!NOTE] 
> Azure Analysis Services database operations use the same SqlServer module as SQL Server Analysis Services. However, not all cmdlets are supported for Azure Analysis Services. To learn more, see [Manage Azure Analysis Services with PowerShell](https://docs.microsoft.com/azure/analysis-services/analysis-services-powershell).
  
##  <a name="bkmk_cmdlets"></a> Analysis Services Cmdlets  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides cmdlets correspond to methods in the **Microsoft.AnalysisServices** namespace. The following table describes each cmdlet and provides a link to the corresponding AMO method.  
  
 If you want to use PowerShell to perform a task that is not represented in the following list (for example to create or synchronize a database), you can write TMSL or XMLA script for that action, and then execute it using the **Invoke-ASCmd** cmdlet.  
  
|Cmdlet|Description|Equivalent AMO Methods|  
|------------|-----------------|----------------------------|  
|[Add-RoleMember cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/Add-RoleMember)|Add a member to a database role.|<xref:Microsoft.AnalysisServices.RoleMemberCollection.Add%2A>|  
|[Backup-ASDatabase cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/backup-asdatabase)|Backup an Analysis Services database.|[Database.Backup](https://msdn.microsoft.com/library/microsoft.analysisservices.database.backup.aspx)|  
|[Invoke-ASCmd cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/invoke-ascmd)|Execute a query or script in XMLA or TSML (JSON) format.|<xref:Microsoft.AnalysisServices.Core.Server.Execute%2A>|  
|[Invoke-ProcessASDatabase](https://docs.microsoft.com/powershell/module/sqlserver/invoke-processasdatabase)|Process a database.|<xref:Microsoft.AnalysisServices.IProcessable.Process%2A>|  
|[Invoke-ProcessCube cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/invoke-processcube)|Process a cube.|<xref:Microsoft.AnalysisServices.IProcessable.Process%2A>|  
|[Invoke-ProcessDimension cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/invoke-processdimension)|Process a dimension.|<xref:Microsoft.AnalysisServices.IProcessable.Process%2A>|  
|[Invoke-ProcessPartition cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/invoke-processpartition)|Process a partition.|<xref:Microsoft.AnalysisServices.IProcessable.Process%2A>|  
|[Invoke-ProcessTable cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/invoke-processtable)|Process a table in a Tabular model, compatibility model 1200 or higher.|<xref:Microsoft.AnalysisServices.IProcessable.Process%2A>|  
|[Merge-Partition cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/merge-partition)|Merge a partition.|<xref:Microsoft.AnalysisServices.Partition.Merge%2A>|  
|[New-RestoreFolder cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/new-restorefolder)|Create a folder to contain a database backup.|<xref:Microsoft.AnalysisServices.RestoreFolder>|  
|[New-RestoreLocation cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/new-restorelocation)|Specify one or more remote servers on which to restore the database.|<xref:Microsoft.AnalysisServices.RestoreLocation>|  
|[Remove-RoleMember cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/remove-rolemember)|Remove a member from a database role.|<xref:Microsoft.AnalysisServices.RoleMemberCollection.Remove%2A>|  
|[Restore-ASDatabase cmdlet](https://docs.microsoft.com/powershell/module/sqlserver/restore-asdatabase)|Restore a database on a server instance.|<xref:Microsoft.AnalysisServices.Core.Server.Restore%2A>|  
  

  
  
