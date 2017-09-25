---
title: "Deploy the Report Viewer web part on a SharePoint site | Microsoft Docs"
ms.custom: ""
ms.date: "09/15/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---

# Deploy the Report Viewer web part on a SharePoint site

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

The Report Viewer Web Part is a custom Web Part that can be used to view SQL Server Reporting Services (native mode) reports within your SharePoint site. You can use the Web Part to view, navigate, print, and export reports on a report server. The Report Viewer Web Part is associated with report definition (.rdl) files that are processed by a SQL Server Reporting Services report server or a Power BI Report Server. This Report Viewer web part cannot be used with Power BI reports hosted in Power BI Report Server.

Use the following instructions to manually deploy the solution package that add the Report Viewer web part to a SharePoint Server 2013 or SharePoint Server 2016 environment. Deploying the solution is a required step for configuring the web part.

**The Report Viewer web part is a standalone solution package and is not associated with SharePoint integrated mode for SQL Server Reporting Services.**

## Requirements

**Supported operating systems:**  
* Windows Server 2008 R2 SP1 and later

**Support SharePoint Server versions:**  
* SharePoint Server 2016
* SharePoint Server 2013

**Support Reporting Services versions:**  
* SQL Server 2008 Reporting Services (Native mode) and later.
* Power BI Report Server

## Download the Report Viewer web part solution package

The Report Viewer web part is available on the Microsoft Download Center.

[Download Report Viewer web part solution package](https://www.microsoft.com/en-us/download/details.aspx?id=55949)

## Deploy the farm solution

This section shows you how to deploy the solution package to your SharePoint farm. This task only needs to be performed once.

1. On a SharePoint server, open a SharePoint Management Shell using the **Run as Administrator** option.

2. Run [Add-SPSolution](https://technet.microsoft.com/library/ff607552(v=office.16).aspx) to add the farm solution.

    ```
    Add-SPSolution –LiteralPath "{path to file}\ReportViewerWebPart.wsp"
    ```

    The cmdlet returns the name of the solution, its solution ID, and Deployed=False. In the next step, you will deploy the solution.

3. Run the [Install-SPSolution](https://technet.microsoft.com/library/ff607534(v=office.16).aspx) cmdlet to deploy the farm solution.

    ```
    Install-SPSolution –Identity ReportViewerWebPart.wsp -GACDeployment -WebApplication {URL to web application}
    ```

## Activate feature

1. In your SharePoint site, select the **gear** icon in the upper left and select **Site Settings*.

    ![Site settings from the gear icon.](media/sharepoint-site-settings.png)

    By default, SharePoint web applications are accessed through port 80. This means that you can often access a SharePoint site by entering *http://<computer name>* to open the root site collection.

3. In **Site Collection Administration**, select **Site collection features**.

4. Scroll down the page until you find the **Report Viewer Web Part** Feature.

5. Select **Activate**.

    ![Activate Report Viewer Web Part feature](media/web-part-activiate-feature.png)

6. Repeat for additional site collections by opening each site and clicking Site Actions.

Optionally, you can also use PowerShell to enable this feature on all sites using the [Enable-SPFeature](https://technet.microsoft.com/library/ff607803.aspx) cmdlet.

```
Get-SPWebApplication "<web application url>" | Get-SPSite -Limit ALL | 
        ForEach-Object {
            Write-Host "Enabling feature for $($_.URL)"
            Enable-SPFeature -identity "ReportViewerWebPart" -URL $_.URL -ErrorAction Continue
        }
```

## Remove the solution

Although SharePoint Central Administration provides solution retraction, you do not need to retract the **ReportViewerWebPart.wsp** file unless you are systematically troubleshooting an installation or patch deployment problem.

### Disable the **Report Viewwer Web Part** feature in each SharePoint site with the Report Viewer Web Part enabled

1. Select the **gear** icon in the upper left and select **Site Settings*.

    ![Site settings from the gear icon.](media/sharepoint-site-settings.png)

    By default, SharePoint web applications are accessed through port 80. This means that you can often access a SharePoint site by entering *http://<computer name>* to open the root site collection.

2. Under **Site Collection Administration**, select **Site collection features**.

3. Click **Deactivate** next to the **Report Viewer Web Part** feature

### Retract the solution from the Farm

1. In SharePoint Central Administration, in **System Settings**, select **Manage farm solutions**.

2. Select **ReportViewerWebPart.wsp**.

3. Select Retract Solution.

### Removing the Solution in PowerShell

Disabling all features and retracting the solution can be done in SharePoint Management Shell by using the Disable-SPFeature, Uninstall-SPSolution, and Remove-SPSolution cmdlets. An example script can be found below:

```
$solutionId = "reportviewerwebpart.wsp"
$featureId = "ReportViewerWebPart"

Write-Host "Searching for solution"
$feature = Get-SPFeature -Identity $featureId -ErrorAction SilentlyContinue
$solution = Get-SPSolution -Identity $solutionId -ErrorAction SilentlyContinue

if ($solution -ne $null) {

    if ($feature -ne $null) {
        Get-SPWebApplication | Get-SPSite -Limit ALL | 
            ForEach-Object {
                Write-Host "Disabling feature for $($_.URL)"
                Disable-SPFeature -identity $featureId -URL $_.URL -confirm:$false -ErrorAction SilentlyContinue
            }    
    }

    if ($solution.Deployed) {
        Write-Host "Uninstalling solution"
        Uninstall-SPSolution -Identity $solutionId -AllWebApplications -confirm:$false

        while ($solution.JobExists) {
            Start-Sleep -Seconds 3
        }
    }

    Write-Host "Removing solution"
    Remove-SPSolution -Identity $solutionId -confirm:$false

    while ($solution.JobExists) {
        Start-Sleep -Seconds 3
    }
}

```

## Next steps

After the Report Viewer web part has been deployed and activiated, you can add the web part to a SharePoint page. For more information, see [Add Report Viewer web part to a SharePoint page](add-report-viewer-web-part-to-page.md).

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)