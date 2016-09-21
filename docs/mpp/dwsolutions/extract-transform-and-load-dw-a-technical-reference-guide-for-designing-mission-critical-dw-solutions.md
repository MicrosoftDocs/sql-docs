---
title: "Extract, Transform, and Load (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5a0530c7-dd50-4eb1-bd69-4e64683387b1
caps.latest.revision: 4
manager: jeffreyg
---
# Extract, Transform, and Load (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Microsoft SQL Server Integration Services (SSIS) is an ETL platform for enterprise-level data integration and data transformation solutions (see <externalLink><linkText>SQL Server Integration Services</linkText><linkUri>http://msdn.microsoft.com/library/ms141026.aspx</linkUri></externalLink><superscript>1</superscript>). Enterprise class development is done via Microsoft Visual Studio, introducing Integration Service Projects as a part of BI solutions. Ad-hoc data imports and exports can be facilitated via SSIS Export/Import Wizard (see <externalLink><linkText>Using the SQL Server Import and Export Wizard to Move Data</linkText><linkUri>http://msdn.microsoft.com/library/ms141209.aspx</linkUri></externalLink><superscript>2</superscript>).</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>The Data Loading Performance Guide</linkText>
              <linkUri>http://msdn.microsoft.com/library/dd425070.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Considerations for High Volume ETL Using SQL Server Integration Services</linkText>
              <linkUri>http://msdn.microsoft.com/library/cc671624.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>Project REAL:</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>Project REAL: Business Intelligence ETL Design Practices</linkText>
                  <linkUri>http://technet.microsoft.com/library/cc966422.aspx</linkUri>
                </externalLink>
                <superscript>5</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Project REAL—Business Intelligence in Practice</linkText>
                  <linkUri>http://www.microsoft.com/sqlserver/2005/en/us/project-real.aspx</linkUri>
                </externalLink>
                <superscript>6</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Establish ETL framework. Same patterns need to be used in packages for loading data into staging, for manipulating data in operational data layer, for dimension, and fact loads as well. This framework will also provide for including ETL lineages and data auditing as it gets loaded and transformed from sources to destinations.</para>
        </listItem>
        <listItem>
          <para>SSIS Framework design is detailed in SQL Server 2008 Integration Services Problem—Design—Solution book on page 33. Another helpful reference to examples of SSIS ETL framework is <externalLink><linkText>Stonemeadow Solutions ETL Framework</linkText><linkUri>http://etlframework.codeplex.com/</linkUri></externalLink>.<superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Top 10 SQL Server Integration Services Best Practices</linkText>
              <linkUri>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkUri>
            </externalLink>
            <superscript>8</superscript>
            <externalLink>
              <linkText />
              <linkUri />
            </externalLink>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Designing Your SSIS Packages for Parallelism (SQL Server Video)</linkText>
              <linkUri>http://msdn.microsoft.com/library/dd795221.aspx</linkUri>
            </externalLink>
            <superscript>9</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Integration Services – SSIS</linkText>
              <linkUri>http://www.sqlis.com/</linkUri>
            </externalLink>
            <superscript>10</superscript> contains multiple links to specific SSIS topics with samples. </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Integration Services (SSIS) – Best Practices</linkText>
              <linkUri>http://www.mssqltips.com/tip.asp?tip=1840</linkUri>
            </externalLink>
            <superscript>11</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Architecture of Integration Services</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb522498.aspx</linkUri>
            </externalLink>
            <superscript>12</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Planning the SQL Server ETL implementation strategy using SSIS for Extracts</linkText>
              <linkUri>http://www.mssqltips.com/tip.asp?tip=1923</linkUri>
            </externalLink>
            <superscript>13</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SSIS architecture: Tips for package design</linkText>
              <linkUri>http://www.infoworld.com/d/data-management/ssis-architecture-tips-package-design-530</linkUri>
            </externalLink>
            <superscript>14</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Designing and Tuning for Performance your SSIS packages in the Enterprise (SQL Video Series)</linkText>
              <linkUri>http://sqlcat.com/presentations/archive/2009/05/02/designing-and-tuning-for-performance-your-ssis-packages-in-the-enterprise-sql-video-series.aspx</linkUri>
            </externalLink>
            <superscript>15</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Examples of successful architectures are described in the following case studies and white papers:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Integration Services Product Samples</linkText>
              <linkUri>http://msftisprodsamples.codeplex.com/</linkUri>
            </externalLink>
            <superscript>16</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Architecture of Integration Services</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb522498.aspx</linkUri>
            </externalLink>
            <superscript>12</superscript>
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
          <para>Determine all connectivity requirements (e.g. connection managers, OLEDB, ODBC, JDBC, and so on).</para>
        </listItem>
        <listItem>
          <para>Build a "sandbox" source system to investigate source system data more effectively.</para>
        </listItem>
        <listItem>
          <para>Establish source version control. Ideally use Microsoft Team Foundation Server. Regardless of the tool used for version control, all versions to SSIS packages need to be archived and accessible in the same manner as all other code in a BI solution. </para>
        </listItem>
        <listItem>
          <para>Introduce Development, Testing, Staging, and Production environments. Make sure versions of SQL server are kept in sync across environments. Implement change control process for ETL components when promoting from Development to Production. In deployment plans always account for rollback procedures. </para>
        </listItem>
        <listItem>
          <para>Build source to target mapping document including relevant business rules for data transformations. Plan this aspect carefully. Volatile source systems (those that change frequently) will require planned effort to keep mapping up-to-date. (See <externalLink><linkText>BI Documenter</linkText><linkUri>http://bidocumenter.com/Public/Screenshots.aspx%23</linkUri></externalLink>.<superscript>17</superscript>)</para>
        </listItem>
        <listItem>
          <para>Will data profiling be required? If so, use SSIS <externalLink><linkText>Data Profiling Task</linkText><linkUri>http://msdn.microsoft.com/library/bb895263.aspx</linkUri></externalLink>.<superscript>18</superscript></para>
        </listItem>
        <listItem>
          <para>Implement SSIS configuration management using combination of environment variables, XML configuration files, and SQL Server configurations. References included on page 38 of SQL Server 2008 Integration Services Problem—Design—Solution book.</para>
        </listItem>
        <listItem>
          <para>Will you have to develop a custom ADO.NET provider to access the source system?</para>
        </listItem>
        <listItem>
          <para>Establish a solid naming convention for SSIS packages, and for SSIS components. Consistency in implementing naming conventions is very important for SSIS auditing and reporting on lineages, as well as for reusability of .dtsx files among developers. </para>
        </listItem>
        <listItem>
          <para>Create SSIS package "templates" with standard variables used by all ETL jobs.</para>
        </listItem>
        <listItem>
          <para>Make sure to use the "OnPostExecute" event handler in the SSIS packages.</para>
        </listItem>
        <listItem>
          <para>Define strategy for deployment and storage. Reference is in Chapter 3 (page 75) of SQL Server 2008 Integration Services Problem—Design—Solution book.</para>
        </listItem>
        <listItem>
          <para>Is there any de-duplication of data required?</para>
        </listItem>
        <listItem>
          <para>What is the default strategy for a package error handler?</para>
        </listItem>
        <listItem>
          <para>How will changes to dimension tables be handled?</para>
        </listItem>
        <listItem>
          <para>Understand source database design (Star Schema, normalized, packaged).</para>
        </listItem>
        <listItem>
          <para>Determine load sequence, frequency, how much data history.</para>
        </listItem>
        <listItem>
          <para>Determine partitioning strategy for large tables.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> SQL Server Integration Services  <externalLink><linkText>http://msdn.microsoft.com/library/ms141026.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms141026.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Using the SQL Server Import and Export Wizard to Move Data <externalLink><linkText>http://msdn.microsoft.com/library/ms141209.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms141209.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> The Data Loading Performance Guide  <externalLink><linkText>http://msdn.microsoft.com/library/dd425070.aspx</linkText><linkUri>http://msdn.microsoft.com/library/dd425070.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Project REAL: Business Intelligence ETL Design Practices  <externalLink><linkText>http://technet.microsoft.com/library/cc966422.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966422.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> DWMantra.com  <externalLink><linkText>http://www.dwmantra.com/dwconcepts.html</linkText><linkUri>http://www.dwmantra.com/dwconcepts.html</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Project REAL—Business Intelligence in Practice  <externalLink><linkText>http://www.microsoft.com/sqlserver/2005/en/us/project-real.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2005/en/us/project-real.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Stonemeadow Solutions ETL Framework  <externalLink><linkText>http://etlframework.codeplex.com/</linkText><linkUri>http://etlframework.codeplex.com/</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Top 10 SQL Server Integration Services Best Practices <externalLink><linkText>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Designing Your SSIS Packages for Parallelism (SQL Server Video) <externalLink><linkText>http://msdn.microsoft.com/library/dd795221.aspx</linkText><linkUri>http://msdn.microsoft.com/library/dd795221.aspx</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> SQL Server Integration Services – SSIS  <externalLink><linkText>http://www.sqlis.com</linkText><linkUri>http://www.sqlis.com/</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> SQL Server Integration Services (SSIS) – Best Practices  <externalLink><linkText>http://www.mssqltips.com/tip.asp?tip=1840</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1840</linkUri></externalLink></para>
      <para>
        <superscript>12</superscript> Architecture of Integration Services  <externalLink><linkText>http://msdn.microsoft.com/library/bb522498.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb522498.aspx</linkUri></externalLink></para>
      <para>
        <superscript>13</superscript> Planning the SQL Server ETL implementation strategy using SSIS for Extracts<superscript /><externalLink><linkText>http://www.mssqltips.com/tip.asp?tip=1923</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1923</linkUri></externalLink></para>
      <para>
        <superscript>14</superscript> SSIS architecture: Tips for package design  <externalLink><linkText>http://www.infoworld.com/d/data-management/ssis-architecture-tips-package-design-530</linkText><linkUri>http://www.infoworld.com/d/data-management/ssis-architecture-tips-package-design-530</linkUri></externalLink></para>
      <para>
        <superscript>15</superscript> Designing and Tuning for Performance your SSIS packages in the Enterprise (SQL Video Series)  <externalLink><linkText>http://sqlcat.com/presentations/archive/2009/05/02/designing-and-tuning-for-performance-your-ssis-packages-in-the-enterprise-sql-video-series.aspx</linkText><linkUri>http://sqlcat.com/presentations/archive/2009/05/02/designing-and-tuning-for-performance-your-ssis-packages-in-the-enterprise-sql-video-series.aspx</linkUri></externalLink></para>
      <para>
        <superscript>16</superscript> SQL Server Integration Services Product Samples  <externalLink><linkText>http://msftisprodsamples.codeplex.com</linkText><linkUri>http://msftisprodsamples.codeplex.com/</linkUri></externalLink></para>
      <para>
        <superscript>17</superscript> BI Documenter  <externalLink><linkText>http://bidocumenter.com/Public/Screenshots.aspx#</linkText><linkUri>http://bidocumenter.com/Public/Screenshots.aspx%23</linkUri></externalLink></para>
      <para>
        <superscript>18</superscript> Data Profiling Task  <externalLink><linkText>http://msdn.microsoft.com/library/bb895263.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb895263.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>