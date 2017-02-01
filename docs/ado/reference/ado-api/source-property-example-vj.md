---
title: "Source Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Source property [ADO], VJ++ example"
ms.assetid: 094ae2f9-9382-4080-ba80-2ad625746ed4
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Source Property Example (VJ++)
This example demonstrates the [Source](../../../ado/reference/ado-api/source-property-ado-recordset.md) property by opening three [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) objects based on different data sources.  
  
```  
// BeginSourceJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class SourceX  
{  
   // The main entry point of the application.  
   public static void main (String[] args)  
   {  
      SourceX();  
      System.exit(0);  
   }  
   // SourceX Function  
   static void SourceX()  
   {  
      // Define string variables.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
                  "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strSQL = "SELECT title_ID AS TitleID, title AS Title,"+  
                "publishers.pub_id AS PubID, pub_name AS PubName "+  
                "FROM publishers INNER JOIN Titles "+  
                "ON publishers.pub_id=Titles.pub_id "+  
                "ORDER BY Title";  
  
      // Define ADO Objects.  
      Connection cnn1 = null;  
      Recordset rstTitles = null;  
      Recordset rstPublishers = null;  
      Recordset rstPublishersDirect = null;  
      Recordset rstTitlesPublishers = null;  
      Command cmdSQL = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         // Open a connection.  
         cnn1 = new Connection();  
         cnn1.open(strCnn);  
  
         // Open a recordset based on a command object.  
         cmdSQL = new Command();  
         cmdSQL.setActiveConnection(cnn1);  
         cmdSQL.setCommandText("Select title,type,pubdate " +  
             "FROM Titles ORDER BY title");  
  
         rstTitles = new Recordset();  
         rstTitles = cmdSQL.execute();  
  
         // Open a recordset based on a table.  
         rstPublishers = new Recordset();  
         rstPublishers.open("publishers", strCnn,   
            AdoEnums.CursorType.FORWARDONLY, AdoEnums.LockType.READONLY,   
            AdoEnums.CommandType.TABLE);  
  
         // Open a recordset based on a table.  
         rstPublishersDirect = new Recordset();  
         rstPublishersDirect.open("publishers", strCnn,   
            AdoEnums.CursorType.FORWARDONLY, AdoEnums.LockType.READONLY,   
            AdoEnums.CommandType.TABLEDIRECT);  
  
         // Open a recordset based on an SQL String.  
         rstTitlesPublishers = new Recordset();  
  
         rstTitlesPublishers.open(strSQL, strCnn,   
            AdoEnums.CursorType.FORWARDONLY, AdoEnums.LockType.READONLY,   
            AdoEnums.CommandType.TEXT);  
  
         // Use Source property to display the source of each recordset.  
         System.out.println("\nrstTitles source: \n"+  
            rstTitles.getSource());  
         System.out.println("\nrstPublishers source: \n"+  
            rstPublishers.getSource());  
         System.out.println("\nrstPublishersDirect source:\n" +  
            rstPublishersDirect.getSource() );  
         System.out.println("\nrstTitlesPublishers source: \n" +  
            rstTitlesPublishers.getSource());  
  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a recordset, check for null pointer first.  
  
         if(rstPublishers != null)  
         {  
            PrintProviderError(rstPublishers.getActiveConnection());  
         }  
         else if(rstPublishersDirect != null)  
         {  
            PrintProviderError(  
               rstPublishersDirect.getActiveConnection());  
         }  
         else if(rstTitles != null)  
         {  
            PrintProviderError(rstTitles.getActiveConnection());  
         }  
         else if(rstTitlesPublishers != null)  
         {  
            PrintProviderError(  
               rstTitlesPublishers.getActiveConnection());  
         }  
         else  
            System.out.println("Exception: " + ae.getMessage());  
  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstTitles != null)  
            if (rstTitles.getState() == 1)  
               rstTitles.close();    
         if (rstPublishers != null)  
            if (rstPublishers.getState() == 1)  
               rstPublishers.close();   
         if (rstPublishersDirect != null)  
            if (rstPublishersDirect.getState() == 1)  
               rstPublishersDirect.close();   
         if (rstTitlesPublishers != null)  
            if (rstTitlesPublishers.getState() == 1)  
               rstTitlesPublishers.close();   
         if (cnn1 != null)  
            if (cnn1.getState() == 1)  
               cnn1.close();  
      }  
  
   }  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndSourceJ  
  
```  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Source Property (ADO Recordset)](../../../ado/reference/ado-api/source-property-ado-recordset.md)