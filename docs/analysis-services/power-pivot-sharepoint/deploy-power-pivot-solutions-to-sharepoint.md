---
title: "Deploy Power Pivot Solutions to SharePoint | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Deploy Power Pivot Solutions to SharePoint
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Use the following instructions to manually deploy two solution packages that add [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] features to a SharePoint Server 2010 environment. Deploying the solutions is a required step for configuring [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint on a SharePoint 2010 server. To view the complete list of required steps, see [Power Pivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md).  
  
 Alternatively, you can use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool to deploy the solutions. Using the configuration tool is easier and more efficient for a single server installation, but you might want to use Central Administration and PowerShell if you prefer using a familiar tool or if you are configuring multiple features at the same time. For more information about using the configuration tool, see [Power Pivot Configuration Tools](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools.md).  
  
 Before deploying the solutions, you must first install [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint using the SQL Server 2012 installation media. SQL Server Setup installs the solution packages that you are about to deploy.  
  
 This topic contains the following sections:  
  
 [Prerequisite: Verify the Web Application uses Classic Mode Authentication](#bkmk_classic)  
  
 [Step 1: Deploy the Farm Solution](#bkmk_farm)  
  
 [Step 2: Deploy the Power Pivot Web Application Solution to Central Administration](#deployCA)  
  
 [Step 3: Deploy the Power Pivot Web Application Solution to Other Web Applications](#deployUI)  
  
 [Redeploy or retract the Solution](#retract)  
  
 [About the Power Pivot Solutions](#intro)  
  
##  <a name="bkmk_classic"></a> Prerequisite: Verify the Web Application uses Classic Mode Authentication  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint is only supported for web applications that use Windows-classic mode authentication. To check whether the application uses Classic mode, run the following PowerShell cmdlet from the **SharePoint 2010 Management Shell**, replacing **http://\<top-level site name>** with the name of your SharePoint site:  
  
```  
Get-spwebapplication http://<top-level site name> | format-list UseClaimsAuthentication  
```  
  
 The return value should be **false**. If it is **true**, you cannot access [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data with this web application.  
  
##  <a name="bkmk_farm"></a> Step 1: Deploy the Farm Solution  
 This section shows you how to deploy solutions using PowerShell, but you can also use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool to complete this task. For more information, see [Configure or Repair Power Pivot for SharePoint 2010 (Power Pivot Configuration Tool)](http://msdn.microsoft.com/d61f49c5-efaa-4455-98f2-8c293fa50046).  
  
 This task only needs to be performed once, after you install [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.  
  
1.  On a server that has an installation of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, open a SharePoint 2010 Management Shell using the **Run as Administrator** option.  
  
2.  Run the following cmdlet to add the farm solution.  
  
    ```  
    Add-SPSolution -LiteralPath "C:\Program Files\Microsoft SQL Server\110\Tools\PowerPivotTools\ConfigurationTool\Resources\PowerPivotFarm.wsp"  
    ```  
  
     The cmdlet returns the name of the solution, its solution ID, and Deployed=False. In the next step, you will deploy the solution.  
  
3.  Run the next cmdlet to deploy the farm solution:  
  
    ```  
    Install-SPSolution -Identity PowerPivotFarm.wsp -GACDeployment -Force  
    ```  
  
##  <a name="deployCA"></a> Step 2: Deploy the Power Pivot Web Application Solution to Central Administration  
 After you deploy the farm solution, you must deploy the Web application solution to Central Administration. This step adds the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard to Central Administration.  
  
1.  Open a SharePoint 2010 Management Shell using the **Run as Administrator** option.  
  
2.  Run the following cmdlet to create a reference to Central Administration:  
  
    ```  
    $centralAdmin = $(Get-SPWebApplication -IncludeCentralAdministration | Where { $_.IsAdministrationWebApplication -eq $TRUE})  
    ```  
  
3.  Run the following cmdlet to add the farm solution.  
  
    ```  
    Add-SPSolution -LiteralPath "C:\Program Files\Microsoft SQL Server\110\Tools\PowerPivotTools\ConfigurationTool\Resources\PowerPivotWebApp.wsp"  
    ```  
  
     The cmdlet returns the name of the solution, its solution ID, and Deployed=False. In the next step, you will deploy the solution.  
  
4.  Run the next cmdlet to install the web application solution to Central Administration.  
  
    ```  
    Install-SPSolution -Identity PowerPivotWebApp.wsp -GACDeployment -Force -WebApplication $centralAdmin  
    ```  
  
 Now that the web application solution is deployed to Central Administration, you can use Central Administration to complete all remaining configuration steps.  
  
##  <a name="deployUI"></a> Step 3: Deploy the Power Pivot Web Application Solution to Other Web Applications  
 In the previous task, you deployed Powerpivotwebapp.wsp to Central Administration. In this section, you deploy the powerpivotwebapp.wsp on each existing web application that supports [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data access. If you add more web applications later, be sure to repeat this step for those additional web applications.  
  
1.  In Central Administration, in System Settings, click **Manage farm solutions**.  
  
2.  Click **powerpivotwebapp.wsp**.  
  
3.  Click **Deploy Solution**.  
  
4.  In **Deploy To?**, select the SharePoint web application for which you want to add [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] feature support.  
  
5.  Click **OK**.  
  
6.  Repeat for other SharePoint web applications that will also support [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data access.  
  
##  <a name="retract"></a> Redeploy or retract the Solution  
 Although SharePoint Central Administration provides solution retraction, you do not need to retract the powerpivotwebapp.wsp file unless you are systematically troubleshooting an installation or patch deployment problem.  
  
1.  In SharePoint 2010 Central Administration, in System Settings, click **Manage farm solutions**.  
  
2.  Click **Powerpivotwebapp.wsp**.  
  
3.  Click **Retract Solution**.  
  
 If you encounter server deployment issues that you trace back to the farm solution, you can redeploy it by running the **Repair** option in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool. Repair operations via the tool is preferred because it requires fewer steps on your part. For more information, see [Configure or Repair Power Pivot for SharePoint 2010 (Power Pivot Configuration Tool)](http://msdn.microsoft.com/d61f49c5-efaa-4455-98f2-8c293fa50046).  
  
 If you still want to re-deploy all solutions, be sure to do so in this order:  
  
1.  Retract the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Web application solution from all SharePoint web applications that use it.  
  
2.  Retract the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] farm solution.  
  
3.  Redeploy the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] farm solution.  
  
4.  Redeploy the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Web application solution to all SharePoint web applications.  
  
##  <a name="intro"></a> About the Power Pivot Solutions  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint uses two solution packages to deploy its application pages and program files to the farm and to individual web applications.  
  
-   The farm solution is global. It is deployed once and then becomes automatically available to any new [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint servers that you add to the farm later.  
  
-   The web application solution is application-specific and must be deployed to each web application in the farm, including the Central Administration web application.  
  
 Each solution is deployed differently.  The farm solution is deployed once, after the first [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint instance is installed. To deploy the farm solution, you use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool or PowerShell cmdlets.  
  
 The web application solution is initially deployed to Central Administration, followed by subsequent solution deployments to any additional web applications that will support requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. To deploy the web application solution to Central Administration, you must use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool or PowerShell cmdlet. For all other web applications, you can deploy the web application solution manually using Central Administration or PowerShell.  
  
|Solution|Description|  
|--------------|-----------------|  
|Powerpivotfarm.wsp|Adds Microsoft.AnalysisServices.SharePoint.Integration.dll to the global assembly.<br /><br /> Adds Microsoft.AnalysisServices.ChannelTransport.dll to the global assembly.<br /><br /> Installs features and resources files, and registers content types.<br /><br /> Adds library templates for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery and Data Feed libraries.<br /><br /> Adds application pages for service application configuration, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard, data refresh, and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery.|  
|Powerpivotwebapp.wsp|Adds Microsoft.AnalysisServices.SharePoint.Integration.dll resources files to the web server extensions folder on the Web front-end.<br /><br /> Adds [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Web service to the Web-front end.<br /><br /> Adds thumbnail image generation for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery.|  
  
## See Also  
 [Upgrade Power Pivot for SharePoint](../../database-engine/install-windows/upgrade-power-pivot-for-sharepoint.md)   
 [Power Pivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md)   
 [Power Pivot Configuration using Windows PowerShell](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md)  
  
  
