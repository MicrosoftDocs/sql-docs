---
title: #Required; Save and load assessments with Azure Synapse Pathway.
description: #Required; Save and load data warehouse assessments with Azure Synapse Pathway.. 
author: #Required; anshul82-ms
ms.author: #Required; anrampal
ms.service: #Required; Azure Synapse Pathway
ms.topic: tutorial #Required; leave this attribute/value as-is.
ms.date: #Required; 02/18/2021 format.
ms.custom: template-tutorial #Required; leave this attribute/value as-is.
---

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

<!--
This template provides the basic structure of a tutorial article.
See the [tutorial guidance](contribute-how-to-mvc-tutorial.md) in the contributor guide.

To provide feedback on this template contact 
[the templates workgroup](mailto:templateswg@microsoft.com).
-->

<!-- 1. H1 
Required. Start with "Tutorial: ". Make the first word following "Tutorial: " a 
verb.
-->

# Save and load assessments with Azure Synapse Translate 

<!-- 2. Introductory paragraph 
Required. Lead with a light intro that describes, in customer-friendly language, 
what the customer will learn, or do, or accomplish. Answer the fundamental “why 
would I want to do this?” question. Keep it short.
-->

The following step-by-step instructions help you use the Azure Synapse Pathway version 1.0 or later to save a data warehouse assessment to a file, and then to load the assessment from a file.

<!-- 3. Tutorial outline 
Required. Use the format provided in the list below.
-->

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Saving an assessment to a file
> * Loading the assessment


<!-- 4. Prerequisites 
Required. First prerequisite is a link to a free trial account if one exists. If there 
are no prerequisites, state that no prerequisites are needed for this tutorial.
-->

## Prerequisites

The tutorial assumes you have already installed the Azure Synapse Pathway MSI. If you need an introduction, see [Perform a Data warehouse code translation with Azure Synapse Pathway](http://www.asp.net/mvc/overview/getting-started/introduction/getting-started).

<!-- 5. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## Saving an assessment to a file
<!-- Introduction paragraph -->

1.After providing the information on the source type & the location of source file and target destination, click on Translate
![Azure Synapse Translate assessment.](./media/saveload-assessment/translate.png)

![Note]
For more information about how to run a translation in Azure Synapse Translate, see the article Perform a SQL Server migration assessment with Data Migration Assistant.

2. You should be able to see the report summarizing the code translation that was performed.
 ![Azure Synapse Translate assessment.](./media/saveload-assessment/view-report.png)
3. Click on Save assessment button, specify the name of the file and then hit Save.
![Azure Synapse Translate assessment.](./media/saveload-assessment/save-assessment.png)

4. A .asmprj file gets created at the specified destination
## Loading an assessment from a file
<!-- Introduction paragraph -->
1. To open the same assessment, click on Load assessment and provide the .asmprj file name
![Azure Synapse Translate assessment.](./media/saveload-assessment/load-assessment.png)

1. The source, input and output folders will be populated based on selected assessment.
![Azure Synapse Translate assessment.](./media/saveload-assessment/load-assessment.png)
1. Click on Translate to re-run the code translation again



## Next steps

Advance to the next article to learn how to create...
> [!div class="nextstepaction"]
> [Next steps button](contribute-how-to-mvc-tutorial.md)

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->