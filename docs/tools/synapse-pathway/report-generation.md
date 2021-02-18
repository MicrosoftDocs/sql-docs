---
title: Report Generation:
description: As a database administrator, I want to visually see the report of what got translated so that I can determine what additional steps I need to take for 100% translation. : 
author: anshul82-ms
ms.author: anrampal
ms.service: Azure Synapse Pathway
ms.topic: tutorial #Required; leave this attribute/value as-is.
ms.date: 02/16/2021
ms.custom: template-tutorial 
---

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

<!--
This template provides the basic structure of a service/product overview article.
See the [overview guidance](contribute-how-write-overview.md) in the contributor guide.

To provide feedback on this template contact 
[the templates workgroup](mailto:templateswg@microsoft.com).
-->

<!-- 1. H1
Required. Set expectations for what the content covers, so customers know the 
content meets their needs. H1 format is # What is <product/service>?
-->

# Report Generation

Azure Synapse Translate provides you with a comprehensive reporting on what scripts were successfully translated, # of errors and warnings on statements that did not get translated 

<!-- 3. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## Generate Report
When you click on Translate, you are presented with a report like the following
![Azure Synapse Pathway report.](./media/report-generaration/report-overview.png)


## Report Summary
The conversion success will show the percentage of scripts translated successfully.
![Azure Synapse pathway.](./media/report-generaration/conversion-success.png)
This section will give the details of the DDL and DML statements that have been translated 
including the number of schemas, tables and database created.
![Azure Synapse report1.](./media/report-generaration/statement-distribution.png)

The migration savings are calculated based on the number of objects translated ( simple, medium and high complex) and assuming 30 mins of manual effort for each object translation.
![Azure Synapse report2.](./media/report-generaration/migration-savings.png)


## Next steps
[Learn more about errors and warnings](../)