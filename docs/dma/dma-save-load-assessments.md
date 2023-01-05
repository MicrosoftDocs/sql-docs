---
title: "Save and load assessments with Data Migration Assistant"
description: Learn how to use Data Migration Assistant to save and load assessments.
author: rajeshsetlem
ms.author: rajpo
ms.date: "01/10/2020"
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Save and load assessments with Data Migration Assistant

The following step-by-step instructions help you use the Data Migration Assistant v5.0 or later to save a database assessment to a file, and then to load an assessment from a file.

> [!NOTE]
> In addition to loading assessments saved using the latest version of DMA, users can also leverage this feature to load assessments exported as .json files from previous versions of Data Migration Assistant to view the results in v5.0 and later.

## Saving an assessment to a file

1. After running an assessment using Data Migration Assistant, select **Save Assessment**.

   ![Opening the Save Assessment dialog box](../dma/media/dma-save-load-assessments/dma-open-save-dialog.png)

   The standard **Save…** dialog box appears.

   > [!NOTE]
   > For more information about how to run an assessment in Data Migration Assistant, see the article [Perform a SQL Server migration assessment with Data Migration Assistant](../dma/dma-assesssqlonprem.md).

2. Specify a name for the file, and then select **Save**.

   ![Naming and saving an assessment file](../dma/media/dma-save-load-assessments/dma-name-save-assessment.png)

## Loading an assessment saved to a file

1. To load an assessment you've previously saved to a file, start Data Migration Assistant, and then on the **All Assessments** tab, select **Load Assessment**.

   ![Opening the Load Assessment dialog box](../dma/media/dma-save-load-assessments/dma-open-load-dialog.png)

2. Navigate to the saved assessment file you want to load, select the file, and then select **Open**.

   ![Opening an assessment file](../dma/media/dma-save-load-assessments/dma-open-assessment.png)

   An entry for the assessment you loaded appears on the **All Assessments** tab.

   ![Displaying assessment entry](../dma/media/dma-save-load-assessments/dma-display-assessment-entry.png)

3. Select the assessment entry, and then select **Open Assessment**.

   ![Opening the assessment detail](../dma/media/dma-save-load-assessments/dma-open-assessment-detail.png)

   Data Migration Assistant now displays details of the assessment you had run previously.

   ![Displaying assessment detail](../dma/media/dma-save-load-assessments/dma-display-assessment-detail.png)
