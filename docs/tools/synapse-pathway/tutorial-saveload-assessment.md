---
title: Save and load assessments with Azure Synapse Pathway.
description: Save and load data warehouse assessments with Azure Synapse Pathway. 
author: anshul82-ms
ms.author: anrampal
ms.prod: sql
ms.technology: "Azure Synapse Pathway"
ms.topic: tutorial 
ms.date: 02/18/2021
---

# Save and load assessments with Azure Synapse Pathway 

The following step-by-step instructions show you how to use Azure Synapse Pathway, version 1.0 or later, to save a data warehouse assessment to a file. You'll then be shown how to load the assessment from a file.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Saving an assessment to a file
> * Loading the assessment

## Prerequisites

To complete this tutorial, make sure you've installed [Azure Synapse Pathway]( If you need an introduction, see [Azure Synapse Pathway overview]azure-synapse-pathway-overview).

## Saving an assessment to a file

1. After entering the information on the source type, the location of source file, and target destination, select **Translate**.
![Azure Synapse Pathway assessment.](./media/saveload-assessment/translate.png)

>[!Note]
>For more information about how to run a translation in Azure Synapse Pathway, see the article Perform a SQL Server migration assessment with Data Migration Assistant.

2. You will see the report summarizing the code translation that was done.
 ![Azure Synapse Pathway assessment.](./media/saveload-assessment/view-report.png)
3. Select the **Save assessment** button, specify the name of the file, and then select **Save**.
![Azure Synapse Pathway assessment.](./media/saveload-assessment/save-assessment.png)

4. A .asmprj file gets created at the specified destination

## Loading an assessment from a file

1. To open the same assessment, select **Load assessment** and provide the .asmprj file name
![Azure Synapse Pathway assessment.](./media/saveload-assessment/load-assessment.png)

1. The source, input, and output folders will be populated based on selected assessment.
![Azure Synapse Pathway assessment.](./media/saveload-assessment/load-assessment.png)
1. Select **Translate** to rerun the code translation again

## Next steps

> [!div class="nextstepaction"]
> [Report Generation](report-generation.md)
