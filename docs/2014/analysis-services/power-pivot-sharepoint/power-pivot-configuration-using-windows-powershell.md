---
title: "PowerPivot Configuration using Windows PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 4d83e53e-04f1-417d-9039-d9e81ae0483d
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Configuration using Windows PowerShell
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] includes Windows PowerShell cmdlets that you can use to configure an installation of [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)]. To fully configure an installation with PowerShell requires the use of both SharePoint cmdlets and PowerPivot for SharePoint cmdlets. A majority of configuration can be completed using one of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] tools. For more information on the tools, see [PowerPivot Configuration Tools](power-pivot-configuration-tools.md).  
  
> [!IMPORTANT]  
>  For a SharePoint 2010 farm, SharePoint 2010 SP1 must be installed before you can configure either PowerPivot for SharePoint, or a SharePoint farm that uses a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database server. If you have not yet installed the service pack, install it before you begin configuring the server.  
  
## Benefits of Configuring PowerPivot for SharePoint Using PowerShell  
 You can build Windows PowerShell script (.ps1) files to automate configuration tasks. This approach is recommended if you require scripted installation and configuration steps that you can run on any server. You might require such a script as part of a disaster recovery plan for rebuilding a server in the event of a hardware failure.  
  
## View a list of the PowerPivot Cmdlets on a Server  
 To see content and examples of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] cmdlets, see [PowerShell Reference for PowerPivot for SharePoint](/sql/analysis-services/powershell/powershell-reference-for-power-pivot-for-sharepoint).  
  
 To use PowerShell to view a list of the PowerPivot cmdlets:  
  
1.  Open SharePoint Management Shell using the **Run as Administrator** option.  
  
2.  Enter the following command:  
  
    ```  
    Get-help *powerpivot*  
    ```  
  
     You should see a list of cmdlets that include PowerPivot in the cmdlet name. For example `Get-PowerPivotServiceApplication`. The number of cmdlets available depends on the version of Analysis Services you are using.  
  
    -   10 cmdlets with SQL Server 2012 SP1 Analysis Services server configured in SharePoint mode, and SharePoint 2013. The 2012 SP1 version utilizes a new architecture that allows the Analysis Server to run outside the SharePoint farm and requires fewer management Windows PowerShell cmdlets.  
  
    -   17 cmdlets with SQL Server 2012 Analysis Services server configured in SharePoint mode, and SharePoint 2010.  
  
     If no commands are returned in the list or you see an error message similar to "`get-help could not find *powerpivot* in a help file in this session.`", see the next section in this topic for instructions on how to enable the PowerPivot cmdlets on the server.  
  
     All cmdlets have online help. The following example shows how to view the online help for the `New-PowerPivotServiceApplication` cmdlet:  
  
    ```  
    Get-help new-powerpivotserviceapplication -full  
    ```  
  
     Alternatively, to view just the examples, use the following syntax:  
  
    ```  
    Get-help new-powerpivotserviceapplication -example  
    ```  
  
## Enable PowerPivot Cmdlets on a Server  
 PowerPivot cmdlets are available after you install PowerPivot for SharePoint and deploy the farm solution. The solutions are deployed when you ran the PowerPivot Configuration tool. Follow these steps to enable the use of cmdlets:  
  
1.  Open SharePoint Management Shell using the **Run as Administrator** option.  
  
2.  Run the first cmdlet:  
  
    ```  
    Add-SPSolution -LiteralPath "C:\Program Files\Microsoft SQL Server\110\Tools\PowerPivotTools\ConfigurationTool\Resources\PowerPivotFarm.wsp"  
    ```  
  
     The cmdlet returns the name of the solution, its solution ID, and Deployed=False. In the next step, you deploy the solution.  
  
3.  Run the second cmdlet to deploy the solution:  
  
    ```  
    Install-SPSolution -Identity PowerPivotFarm.wsp -GACDeployment -Force  
    ```  
  
4.  Close the window. Reopen it, again using the **Run as Administrator** option.  
  
## Related Content  
 [PowerPivot Server Administration and Configuration in Central Administration](power-pivot-server-administration-and-configuration-in-central-administration.md)  
  
 [PowerPivot Configuration Tools](power-pivot-configuration-tools.md)  
  
 [PowerShell Reference for PowerPivot for SharePoint](/sql/analysis-services/powershell/powershell-reference-for-power-pivot-for-sharepoint)  
  
  
