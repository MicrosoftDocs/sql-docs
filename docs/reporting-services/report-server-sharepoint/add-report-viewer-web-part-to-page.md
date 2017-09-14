---
title: "Add Report Viewer web part to a SharePoint page | Microsoft Docs"
ms.custom: "Add the Report Viewer web part to a page within your SharePoint site."
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

# Add Report Viewer web part to a SharePoint page

Add the Report Viewer web part to a page within your SharePoint site.

## Add web part

1. In your SharePoint site, select the **gear** icon in the upper left and select **Add a page**.

2. Give your page a name and select **Create**.

3. Within the page designer, select the **Insert** tab in the ribbon. Then select **Web part** within the **Parts** section.

4. Under **Categories**, select **SQL Server REporting Services (Native mode). Under **Parts**, select **Report Viewer**. Then select **Add**.

    This may initially appear with an error. The error is because the default report server URl is set to *http://localhost* and may not be available at that location.

## Configure the Report Viewer web part

To configure the web part to point to your specific report, do the following.

1. When editing the SharePoint page, select the down arrow in the upper right of the web part and select **Edit Web part**.

2. 

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)