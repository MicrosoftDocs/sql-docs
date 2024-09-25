---
title: "Save and load assessments with Data Migration Assistant"
description: Learn how to use Data Migration Assistant to save and load assessments.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Save and load assessments with Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

The following step-by-step instructions help you use the Data Migration Assistant v5.0 or later to save a database assessment to a file, and then to load an assessment from a file.

In addition to loading assessments saved using the latest version of DMA, users can also use this feature to load assessments exported as .json files from previous versions of Data Migration Assistant to view the results in v5.0 and later.

## Save an assessment to a file

1. After running an assessment using Data Migration Assistant, select **Save Assessment**.

   :::image type="content" source="media/dma-save-load-assessments/dma-open-save-dialog.png" alt-text="Screenshot of Opening the Save Assessment dialog box." lightbox="media/dma-save-load-assessments/dma-open-save-dialog.png":::

   The standard **Save...** dialog box appears.

   > [!NOTE]  
   > For more information about how to run an assessment in Data Migration Assistant, see the article [Perform a SQL Server migration assessment with Data Migration Assistant](dma-assesssqlonprem.md).

1. Specify a name for the file, and then select **Save**.

   :::image type="content" source="media/dma-save-load-assessments/dma-name-save-assessment.png" alt-text="Screenshot of Naming and saving an assessment file." lightbox="media/dma-save-load-assessments/dma-name-save-assessment.png":::

## Load an assessment saved to a file

1. To load an assessment you've previously saved to a file, start Data Migration Assistant, and then on the **All Assessments** tab, select **Load Assessment**.

   :::image type="content" source="media/dma-save-load-assessments/dma-open-load-dialog.png" alt-text="Screenshot of Opening the Load Assessment dialog box." lightbox="media/dma-save-load-assessments/dma-open-load-dialog.png":::

1. Navigate to the saved assessment file you want to load, select the file, and then select **Open**.

   :::image type="content" source="media/dma-save-load-assessments/dma-open-assessment.png" alt-text="Screenshot of Opening an assessment file." lightbox="media/dma-save-load-assessments/dma-open-assessment.png":::

   An entry for the assessment you loaded appears on the **All Assessments** tab.

   :::image type="content" source="media/dma-save-load-assessments/dma-display-assessment-entry.png" alt-text="Screenshot of Displaying assessment entry." lightbox="media/dma-save-load-assessments/dma-display-assessment-entry.png":::

1. Select the assessment entry, and then select **Open Assessment**.

   :::image type="content" source="media/dma-save-load-assessments/dma-open-assessment-detail.png" alt-text="Screenshot of Opening the assessment detail." lightbox="media/dma-save-load-assessments/dma-open-assessment-detail.png":::

   Data Migration Assistant now displays details of the assessment you had run previously.

   :::image type="content" source="media/dma-save-load-assessments/dma-display-assessment-detail.png" alt-text="Screenshot showing assessment detail." lightbox="media/dma-save-load-assessments/dma-display-assessment-detail.png":::
