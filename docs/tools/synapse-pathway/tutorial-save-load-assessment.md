---
title: Save and load assessments with Azure Synapse Pathway
description: Save and load data warehouse assessments with Azure Synapse Pathway
author: WilliamDAssafMSFT 
ms.author: wiassaf 
ms.service: sql
ms.subservice: tools-other
ms.topic: tutorial 
ms.date: 03/02/2021
monikerRange: "=azure-sqldw-latest"
---

# Save and load assessments with Azure Synapse Pathway
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

The following step-by-step instructions demonstrate how to save and upload a data warehouse assessment from a file using Azure Synapse Pathway.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Save an assessment to a file
> * Load the assessment from a file

## Prerequisites

To complete this tutorial, make sure you've installed [Azure Synapse Pathway](synapse-pathway-download.md) See [Azure Synapse Pathway overview](azure-synapse-pathway-overview.md) to learn more about the tool.

## Saving an assessment to a file

1. Once you have run the translation, you should see the report summarizing the code translation 
 ![Azure Synapse Pathway assessment report overview.](./media/tutorial-save-load-assessment/report-overview.png)
1. Select the **Save assessment** button, specify the name of the file, and then select **Save**.
![Azure Synapse Pathway assessment.](./media/tutorial-save-load-assessment/save-assessment.png)

1. A .asmprj file is created at the specified destination.

## Loading an assessment from a file

1. To open the same assessment, select **Load assessment** and provide the .asmprj file name
![Azure Synapse Pathway browse to assessment location.](./media/tutorial-save-load-assessment/browse-location.png)

1. The source, input, and output folders will be populated based on selected assessment.
![Azure Synapse Pathway assessment configuration showing translation type, input directory, and output directory.](./media/tutorial-save-load-assessment/load-assessment.png)
1. Select **Translate** to rerun the code translation again

## Next steps

> [!div class="nextstepaction"]
> [Report Generation](report-generation.md)
