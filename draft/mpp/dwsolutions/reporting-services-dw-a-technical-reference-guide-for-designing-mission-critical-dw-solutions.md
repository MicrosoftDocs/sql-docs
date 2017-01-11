---
title: "Reporting Services (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: dd2da9b2-f1c8-4897-a28b-ad3a9027a59a
caps.latest.revision: 4
manager: jeffreyg
---
# Reporting Services (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Standard reports are the crux of BI applications and the foundation from which most companies run their data warehouse. They are typically predefined-format, parameter-driven reports. These reports are work-horse BI applications of the business. These are the reports people look at every day.</para>
    <para>In more advanced applications, SSRS has been deployed as an integral part of the corporate reporting infrastructure. As such, reports are often pre-cached and/or advanced data warehouse aggregating techniques are used to dramatically enhance the performance of reporting and reduce the burden of ad-hoc reporting on the data warehouse.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Report Server Catalog Best Practices</linkText>
              <linkUri>http://sqlcat.com/technicalnotes/archive/2008/06/26/report-server-catalog-best-practices.aspx</linkUri>
            </externalLink>
            <superscript>1</superscript>: This technical note is part of the <externalLink><linkText>Reporting Services Scale-Out Architecture</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/06/05/reporting-services-scale-out-architecture.aspx</linkUri></externalLink><superscript>2</superscript> which provides general guidance on how to set up, implement, and optimize enterprise scale-out architecture for your Microsoft SQL Server Reporting Services (SSRS) environment. This note provides guidance for both SQL Server 2005 and 2008 Reporting Services. </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Planning a Deployment Mode</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb326345.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>: Should you use native or SharePoint for report deployment? </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following provide helpful information:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Component Architecture</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb522673.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Service Architecture (Reporting Services)</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb630409.aspx</linkUri>
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
      <para>Following are some question and consideration you can use when working with customers.</para>
      <list class="bullet">
        <listItem>
          <para>Reporting is a very generic term and often misrepresented. It is imperative to carefully understand an organization’s reporting strategy completely including:</para>
          <list class="bullet">
            <listItem>
              <para>Ad-hoc versus pre-canned reports</para>
            </listItem>
            <listItem>
              <para>Data latency</para>
            </listItem>
            <listItem>
              <para>Deployment strategy (e.g. SharePoint)</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Define contents, layout and formatting of the reports.</para>
        </listItem>
        <listItem>
          <para>Identify and establish Development, Testing, and Production environments for managing development and deployment of reports.</para>
        </listItem>
        <listItem>
          <para>Developers will manage report development via BIDS (Visual Studio), while information users can use Report Builder 3.0 to author reports. Here is the link to getting starting with using SSRS: <externalLink><linkText>Getting Started with Report Builder 3.0</linkText><linkUri>http://technet.microsoft.com/library/dd220460.aspx</linkUri></externalLink><superscript>6</superscript></para>
        </listItem>
        <listItem>
          <para>Establish deployment and rollback procedures for SSRS reports as a part of overall development strategy.</para>
        </listItem>
        <listItem>
          <para>Integrate SSIS project in Visual Studio with other BI projects (SSIS, SSAS) for a comprehensive BI Solution.</para>
        </listItem>
        <listItem>
          <para>It is highly recommended to introduce report development version control early on. </para>
        </listItem>
        <listItem>
          <para>Use integration with Source Control systems, preferably with Team Foundation Server 2010: <externalLink><linkText>Team Foundation Server 2010</linkText><linkUri>http://msdn.microsoft.com/vstudio/ff637362</linkUri></externalLink><superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>Build SSRS templates, and distribute them to development team members. This is very important for development involving multiple developers, often located at different geographical locations, to maintain consistency in presentation and adherence to corporate standards and regulations.</para>
        </listItem>
        <listItem>
          <para>Consider integrating SSRS Report Viewer in applications for branding and conforming to corporate standards: <externalLink><linkText>Reporting Services and ReportViewer Controls in Visual Studio 2010</linkText><linkUri>http://msdn.microsoft.com/library/ms345248.aspx</linkUri></externalLink><superscript>8</superscript></para>
        </listItem>
        <listItem>
          <para>Define the execution options for reports—scheduled, interactive.</para>
        </listItem>
        <listItem>
          <para>Define the distribution options for reports: web, email, file directory, and so on.</para>
        </listItem>
        <listItem>
          <para>Participate in existing security systems, and potentially augment them at the user/report level.</para>
        </listItem>
        <listItem>
          <para>Facilitate information collected in SSRS service databases to provide insight in reports metadata and information on user activity, report execution times, and so on. See <externalLink><linkText>Power of the ReportServer – How to pull data from Reporting Services Database</linkText><linkUri>http://weblogs.sqlteam.com/jhermiz/archive/2007/08/14/60285.aspx</linkUri></externalLink>.<superscript>9</superscript></para>
        </listItem>
        <listItem>
          <para>Use report execution times to proactively tune reports for optimum performance to maintain high end-user satisfaction level.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Report Server Catalog Best Practices <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2008/06/26/report-server-catalog-best-practices.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/06/26/report-server-catalog-best-practices.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Reporting Services Scale-Out Architecture  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2008/06/05/reporting-services-scale-out-architecture.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/06/05/reporting-services-scale-out-architecture.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Planning a Deployment Mode  <externalLink><linkText>http://msdn.microsoft.com/library/bb326345.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb326345.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Component Architecture  <externalLink><linkText>http://msdn.microsoft.com/library/bb522673.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb522673.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Service Architecture (Reporting Services)  <externalLink><linkText>http://msdn.microsoft.com/library/bb630409.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb630409.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Getting Started with Report Builder 3.0 <externalLink><linkText>http://technet.microsoft.com/library/dd220460.aspx</linkText><linkUri>http://technet.microsoft.com/library/dd220460.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Team Foundation Server 2010  <externalLink><linkText>http://msdn.microsoft.com/vstudio/ff637362</linkText><linkUri>http://msdn.microsoft.com/vstudio/ff637362</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Reporting Services and ReportViewer Controls in Visual Studio 2010 <externalLink><linkText>http://msdn.microsoft.com/library/ms345248.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms345248.aspx</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Power of the ReportServer – How to pull data from Reporting Services Database  <externalLink><linkText>http://weblogs.sqlteam.com/jhermiz/archive/2007/08/14/60285.aspx</linkText><linkUri>http://weblogs.sqlteam.com/jhermiz/archive/2007/08/14/60285.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>