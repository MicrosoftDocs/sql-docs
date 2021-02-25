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

# Save and load assessments with Azure Synapse Translate 

The following step-by-step instructions show you how to use Azure Synapse Pathway, version 1.0 or later, to save a data warehouse assessment to a file. You'll then be shown how to load the assessment from a file.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Saving an assessment to a file
> * Loading the assessment

## Prerequisites

The tutorial assumes you've already installed the Azure Synapse Pathway MSI. If you need an introduction, see [Perform a Data warehouse code translation with Azure Synapse Pathway](http://www.asp.net/mvc/overview/getting-started/introduction/getting-started).

## Saving an assessment to a file

1. After entering the information on the source type, the location of source file, and target destination, select **Translate**.
![Azure Synapse Translate assessment.](./media/saveload-assessment/translate.png)

>[!Note]
>For more information about how to run a translation in Azure Synapse Translate, see the article Perform a SQL Server migration assessment with Data Migration Assistant.

2. You will see the report summarizing the code translation that was done.
 ![Azure Synapse Translate assessment.](./media/saveload-assessment/view-report.png)
3. Select the **Save assessment** button, specify the name of the file, and then select **Save**.
![Azure Synapse Translate assessment.](./media/saveload-assessment/save-assessment.png)

4. A .asmprj file gets created at the specified destination

## Loading an assessment from a file

1. To open the same assessment, select **Load assessment** and provide the .asmprj file name
![Azure Synapse Translate assessment.](./media/saveload-assessment/load-assessment.png)

1. The source, input, and output folders will be populated based on selected assessment.
![Azure Synapse Translate assessment.](./media/saveload-assessment/load-assessment.png)
1. Select **Translate** to rerun the code translation again

## Next steps

Advance to the next article to learn how to create...
> [!div class="nextstepaction"]
> [Next steps button](contribute-how-to-mvc-tutorial.md)
