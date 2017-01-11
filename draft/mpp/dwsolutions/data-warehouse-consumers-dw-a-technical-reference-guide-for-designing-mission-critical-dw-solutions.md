---
title: "Data Warehouse Consumers (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f3af44d3-9022-46ac-a907-8a8d5ce8917a
caps.latest.revision: 4
manager: jeffreyg
---
# Data Warehouse Consumers (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
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
    <para>Consumers represent technology that allows end-users to access information derived from the data warehouse. An important distinction is that, typically the raw data are stored in the warehouse, but it is the consumers that transform the raw data into information that can actually be used by the business. Consumers provide an environment raw data may be transformed (decoded or enriched), and delivered in a form that is more easily analyzed (Business Intelligence [BI]). Typical data consumers include:</para>
    <list class="bullet">
      <listItem>
        <para>Tools/programs for extracting data from sources, transforming and loading (ETL)</para>
      </listItem>
      <listItem>
        <para>Tools to execute queries and reports</para>
      </listItem>
      <listItem>
        <para>Online Analytical Programs (OLAP) cube structures to quickly "slice and dice" data for analysis</para>
      </listItem>
      <listItem>
        <para>A BI semantic layer to capture metadata (data definitions, and data lineage, for example)</para>
      </listItem>
      <listItem>
        <para>Suites of interrelated applications and services</para>
      </listItem>
    </list>
  </introduction>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>What's new for PerformancePoint Services (SharePoint Server 2010)</linkText>
              <linkUri>http://technet.microsoft.com/en-us/library/ee661741.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>An excellent tutorial for <externalLink><linkText>Microsoft Power Pivot</linkText><linkUri>http://www.powerpivot.com/</linkUri></externalLink><superscript>4</superscript></para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Reporting Services</linkText>
              <linkUri>http://www.microsoft.com/sqlserver/2008/en/us/reporting.aspx</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
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
          <para>Enforce <externalLink><linkText>Single version of truth</linkText><linkUri>http://en.wikipedia.org/wiki/Single_version_of_the_truth</linkUri></externalLink><superscript>6</superscript> for all data being consumed from any data layer in data warehouse. </para>
        </listItem>
        <listItem>
          <para>Understand security requirements for data access:</para>
          <list class="bullet">
            <listItem>
              <para>Consider role-based security</para>
            </listItem>
            <listItem>
              <para>What is feasibility of integration with Active Directory?</para>
            </listItem>
            <listItem>
              <para>Are there integration point with custom security models</para>
            </listItem>
            <listItem>
              <para>What are column and row based security requirements?</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Understand Microsoft’s vision—see <externalLink><linkText>Business Intelligence to the Masses With SharePoint</linkText><linkUri>http://www.microsoft.com/presspass/features/2009/jan09/01-27kurtdelbeneqa.mspx</linkUri></externalLink>.<superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>Determine all connectivity requirements (e.g. connection managers, OLEDB, ODBC, JDBC, and so on).</para>
        </listItem>
        <listItem>
          <para>Understand source database design (Star Schema, normalized).</para>
        </listItem>
        <listItem>
          <para>For more effective management of data consumptions, consider classifying consumers as: Queries, Reports, BI Semantic Layer Tools, Embedded BI Applications, Suites of Applications and Services, and External Data Feeds .</para>
        </listItem>
        <listItem>
          <para>Determine if the client will be using third-party BI products (e.g. MicroStrategy, Business Objects, Cognos) or will they use the Microsoft stack exclusively.</para>
        </listItem>
        <listItem>
          <para>Understand the consumer deployment model(s) (e.g. will end-users simply run canned analysis/reports, or will they have direct access to the database?).</para>
        </listItem>
        <listItem>
          <para>What is the refresh rate of consumer data (e.g. how frequently does it need to be updated)?</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1 </superscript>Top 10 Performance and Productivity Reasons to Use SQL Server 2008 for Your Business Intelligence Solutions <externalLink><linkText> http://sqlcat.com/top10lists/archive/2009/02/24/top-10-performance-and-productivity-reasons-to-use-sql-server-2008-for-your-business-intelligence-solutions.aspx</linkText><linkUri>%20http://sqlcat.com/top10lists/archive/2009/02/24/top-10-performance-and-productivity-reasons-to-use-sql-server-2008-for-your-business-intelligence-solutions.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2 </superscript>SQL Server Books Online <externalLink><linkText>msdn.microsoft.com/en-us/library/ms130214.aspx</linkText><linkUri>http://msdn.microsoft.com/en-us/library/ms130214.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> What's new for PerformancePoint Services (SharePoint Server 2010) <externalLink><linkText>http://technet.microsoft.com/en-us/library/ee661741.aspx</linkText><linkUri>http://technet.microsoft.com/en-us/library/ee661741.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript>
        <token>ssGemini</token>
        <externalLink>
          <linkText>http://www.powerpivot.com/</linkText>
          <linkUri>http://www.powerpivot.com/</linkUri>
        </externalLink>
      </para>
      <para>
        <superscript>5</superscript> SQL Server2008 Reporting Services <externalLink><linkText>http://www.microsoft.com/sqlserver/2008/en/us/reporting.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/reporting.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Single version of truth <externalLink><linkText>http://en.wikipedia.org/wiki/Single_version_of_the_truth</linkText><linkUri>http://en.wikipedia.org/wiki/Single_version_of_the_truth</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Business Intelligence to the Masses with SharePoint <externalLink><linkText>http://www.microsoft.com/presspass/features/2009/jan09/01-27kurtdelbeneqa.mspx</linkText><linkUri>http://www.microsoft.com/presspass/features/2009/jan09/01-27kurtdelbeneqa.mspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>