---
title: Save and load assessments with Azure Synapse Pathway (Preview).
description: Save and load data warehouse assessments with Azure Synapse Pathway (Preview). 
author: anshul82-ms
ms.author: anrampal
ms.prod: sql
ms.technology: "Azure Synapse Pathway"
ms.topic: tutorial 
ms.date: 02/18/2021
---

# Save and load assessments with Azure Synapse Pathway (Preview) 

The following step-by-step instructions demonstrate how to save and upload a data warehouse assessment from a file using Azure Synapse Pathway.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Saving an assessment to a file
> * Loading the assessment from a file

## Prerequisites

To complete this tutorial, make sure you've installed [Azure Synapse Pathway](synapse-pathway-download.md) See [Azure Synapse Pathway overview](azure-synapse-pathway-overview.md) to learn more about the tool.

## Saving an assessment to a file
 
1. Once you have run the translation, you should see the report summarizing the code translation 
 ![Azure Synapse Pathway assessment.](./media/save-load-assessment/report-overview.png)
3. Select the **Save assessment** button, specify the name of the file, and then select **Save**.
![Azure Synapse Pathway assessment.](./media/save-load-assessment/save-assessment.png)

4. A .asmprj file gets created at the specified destination

## Loading an assessment from a file

1. To open the same assessment, select **Load assessment** and provide the .asmprj file name
![Azure Synapse Pathway assessment.](./media/save-load-assessment/browse-location.png)

1. The source, input, and output folders will be populated based on selected assessment.
![Azure Synapse Pathway assessment.](./media/save-load-assessment/load-assessment.png)
1. Select **Translate** to rerun the code translation again

## Next steps

> [!div class="nextstepaction"]
> [Report Generation](report-generation.md)
