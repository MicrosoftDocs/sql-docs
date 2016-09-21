---
title: "PowerPivot (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f9202cfc-a968-4c3d-bc50-c2c3d53339f7
caps.latest.revision: 3
manager: jeffreyg
---
# PowerPivot (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <summary />
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/en-us/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Microsoft <token>ssGemini</token> is the latest, and one of the most exciting, members of the Microsoft BI stack. If used properly, it can empower end-users to define their own analytics in common interfaces such as Microsoft Excel. </para>
    <para>
      <token>ssGemini</token> provides numerous capabilities and features:</para>
    <list class="bullet">
      <listItem>
        <para>Extends Excel and SharePoint to create a self-service BI system.</para>
      </listItem>
      <listItem>
        <para>Creates applications inside Excel 2010.</para>
      </listItem>
      <listItem>
        <para>A server-side component that enhances SharePoint 2010 with the capability to share those applications across the organization.</para>
      </listItem>
      <listItem>
        <para>Allows applications to be updated with the latest data, and monitors how people are using them.</para>
      </listItem>
    </list>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Power Pivot Overview</linkText>
              <linkUri>http://msdn.microsoft.com/en-us/library/ee210692.aspx</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Power Pivot Technical Diagram: Power Pivot Client/Server Architecture</linkText>
              <linkUri>http://sqlcat.com/whitepapers/archive/2010/04/23/powerpivot-technical-diagram-powerpivot-client-server-architecture.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Microsoft SQL Server 2008 R2 Power Pivot Planning and Deployment</linkText>
              <linkUri>http://sqlcat.com/whitepapers/archive/2010/04/14/microsoft-sql-server-2008-r2-powerpivot-planning-and-deployment.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Power Pivot Technical Diagram: Power Pivot Security Architecture</linkText>
              <linkUri>http://sqlcat.com/whitepapers/archive/2010/08/17/powerpivot-technical-diagram-powerpivot-security-architecture.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Server Installation</linkText>
              <linkUri>http://powerpivotgeek.com/server-installation/</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Power Pivot part 1</linkText>
              <linkUri>http://technet.microsoft.com/en-us/edge/video/ff711395</linkUri>
            </externalLink>
            <superscript>6</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Power Pivot for SharePoint – Existing Farm Installation</linkText>
              <linkUri>http://sqlcat.com/whitepapers/archive/2010/09/07/powerpivot-for-sharepoint-existing-farm-installation.aspx</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Powerpivot-info.com</linkText>
              <linkUri>http://www.powerpivot-info.com/</linkUri>
            </externalLink>
            <superscript>8</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>The Great Power Pivot FAQ</linkText>
              <linkUri>http://powerpivotfaq.com/Lists/TGPPF/AllItems.aspx</linkUri>
            </externalLink>
            <superscript>9</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Microsoft Power Pivot for Excel and SharePoint</linkText>
              <linkUri>http://www.amazon.com/Professional-Microsoft-PowerPivot-SharePoint-Programmer/dp/0470587377/ref=sr_1_3?ie=UTF8&amp;qid=1294947867&amp;sr=8-3</linkUri>
            </externalLink>
            <superscript>10</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Examples of successful architectures are described in the following case studies and white papers.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Mediterranean Shipping Company: Shipping Company Makes Critical Data Available Faster with New BI Tools</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000006944</linkUri>
            </externalLink>
            <superscript>11</superscript>: Mediterranean Shipping Company (MSC) is the second-largest container ship line in the world based on volume. MSC was especially excited about the extensive support for self-service BI in SQL Server 2008 R2, particularly with Microsoft SQL Server <token>ssGemini</token> for Microsoft Excel. By using this free, downloadable BI technology, MSC users will be able to work with massive volumes of data in their Microsoft Excel 2010.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>BNZ: New Zealand Bank Turns to New BI Tools for Delivering Vital Branch Performance Data Processes</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000007015</linkUri>
            </externalLink>
            <superscript>12</superscript>: <token>ssGemini</token> for Excel makes it possible for users to perform BI tasks on hundreds of millions of rows of data using Microsoft Excel 2010, which is part of Microsoft Office 2010 Professional. BNZ plans to deploy Excel 2010 in the Finance Department in late 2010.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>CareGroup Healthcare System: Healthcare Group to Enhance Information Access with Powerful Business Intelligence Tools</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000007023</linkUri>
            </externalLink>
            <superscript>13</superscript>: In late 2009, CareGroup began a pilot of the <token>ssGemini</token> technologies involving 10 employees. The SQL Server 2008 R2 solution is expected to become available to a broad base of employees by the second half of 2010, and full deployment is scheduled to begin in late 2010.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>
            <token>ssGemini</token> empowers end users to build their own analytical apps, but this power must be deployed with caution as end-users may not be familiar with raw data characteristics (e.g. averaging an average).</para>
        </listItem>
        <listItem>
          <para>Understand the sizes of data being analyzed. Average compression ratios are 15:1, but ratios as high as 1000:1 are not uncommon. A 2GB dataset will take 4GB of memory because of required memory operations, but remember that 2GB represents a lot of data in <token>ssGemini</token>. </para>
        </listItem>
        <listItem>
          <para>Requires "power" end users who are comfortable with Excel and multi-dimensional data structures.</para>
        </listItem>
        <listItem>
          <para>Since it is an end-user tool, data governance and control requirements may have to be redefined.</para>
        </listItem>
        <listItem>
          <para>Excel governance and compliance rules might need to be put into place to manage the <token>ssGemini</token> spreadsheets.</para>
        </listItem>
        <listItem>
          <para>Each table must have a single column that uniquely identifies each row in that table. Therefore, the schema used for analysis must be known to meet these criteria.</para>
        </listItem>
        <listItem>
          <para>When using SSAS as a source, realize that <token>ssGemini</token> still makes a copy of the data and builds its own in-memory cube.</para>
        </listItem>
        <listItem>
          <para>Create a SharePoint strategy for <token>ssGemini</token> document management and workflow.</para>
        </listItem>
        <listItem>
          <para>Consider ramifications of Office 2010 32-bit versus 64-bit.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript>
        <token>ssGemini</token> Overview  <externalLink><linkText>msdn.microsoft.com/en-us/library/ee210692.aspx</linkText><linkUri>http://msdn.microsoft.com/en-us/library/ee210692.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript>
        <token>ssGemini</token> Technical Diagram: <token>ssGemini</token> Client/Server Architecture  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/04/23/powerpivot-technical-diagram-powerpivot-client-server-architecture.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/04/23/powerpivot-technical-diagram-powerpivot-client-server-architecture.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Microsoft SQL Server 2008 R2 <token>ssGemini</token> Planning and Deployment <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/04/14/microsoft-sql-server-2008-r2-powerpivot-planning-and-deployment.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/04/14/microsoft-sql-server-2008-r2-powerpivot-planning-and-deployment.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript>
        <token>ssGemini</token> Technical Diagram: <token>ssGemini</token> Security Architecture <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/08/17/powerpivot-technical-diagram-powerpivot-security-architecture.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/08/17/powerpivot-technical-diagram-powerpivot-security-architecture.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Server Installation <externalLink><linkText>http://powerpivotgeek.com/server-installation</linkText><linkUri>http://powerpivotgeek.com/server-installation/</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript>
        <token>ssGemini</token> part 1 <externalLink><linkText>http://technet.microsoft.com/en-us/edge/video/ff711395</linkText><linkUri>http://technet.microsoft.com/en-us/edge/video/ff711395</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript>
        <token>ssGemini</token> for SharePoint – Existing Farm Installation <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/09/07/powerpivot-for-sharepoint-existing-farm-installation.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/09/07/powerpivot-for-sharepoint-existing-farm-installation.aspx</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Powerpivot-info.com  <externalLink><linkText>http://www.powerpivot-info.com/</linkText><linkUri>http://www.powerpivot-info.com/</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> The Great <token>ssGemini</token> FAQ  <externalLink><linkText>http://powerpivotfaq.com/Lists/TGPPF/AllItems.aspx</linkText><linkUri>http://powerpivotfaq.com/Lists/TGPPF/AllItems.aspx</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> Microsoft <token>ssGemini</token> for Excel and SharePoint  <externalLink><linkText>http://www.amazon.com/Professional-Microsoft-PowerPivot-SharePoint-Programmer/dp/0470587377/ref=sr_1_3?ie=UTF8&amp;qid=1294947867&amp;sr=8-3</linkText><linkUri>http://www.amazon.com/Professional-Microsoft-PowerPivot-SharePoint-Programmer/dp/0470587377/ref=sr_1_3?ie=UTF8&amp;qid=1294947867&amp;sr=8-3</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> Mediterranean Shipping Company: Shipping Company Makes Critical Data Available Faster with New BI Tools <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000006944</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000006944</linkUri></externalLink></para>
      <para>
        <superscript>12</superscript> BNZ: New Zealand Bank Turns to New BI Tools for Delivering Vital Branch Performance Data Processes <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000007015</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000007015</linkUri></externalLink></para>
      <para>
        <superscript>13</superscript> CareGroup Healthcare System: Healthcare Group to Enhance Information Access with Powerful Business Intelligence Tools <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000007023</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=4000007023</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>