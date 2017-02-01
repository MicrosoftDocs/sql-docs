---
title: "Prepared Property Example (VJ++) | Microsoft Docs"
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
  - "Prepared property [ADO], VJ++ example"
ms.assetid: fc52ea7c-1b3b-4874-9db9-4d2e01d794c3
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Prepared Property Example (VJ++)
This example demonstrates the [Prepared](../../../ado/reference/ado-api/prepared-property-ado.md) property by opening two [Command](../../../ado/reference/ado-api/command-object-ado.md) objects—one prepared and one not prepared.  
  
```  
// BeginPreparedJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class PreparedX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      PreparedX();  
      System.exit(0);  
   }  
  
   // PreparedX function  
  
   static void PreparedX()  
   {  
      // Define string variables.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
            + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strCmd = "SELECT title, type FROM Titles ORDER BY type";  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Command cmd1 = null;  
      Command cmd2 = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      long timeStart;  
      long timeEnd;  
      float timeNotPrepared ;  
      float timePrepared;  
      int   intLoop;  
      String strTemp;  
  
      try  
      {  
         // Open a connection.  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
  
         // Create two command objects for the same  
         // command - one prepared and one not prepared.  
         cmd1 = new Command();  
         cmd1.setActiveConnection(cnConn1);  
         cmd1.setCommandType(AdoEnums.CommandType.TEXT);  
         cmd1.setCommandText(strCmd);  
  
         cmd2 = new Command();  
         cmd2.setActiveConnection(cnConn1);  
         cmd2.setCommandType(AdoEnums.CommandType.TEXT);  
         cmd2.setCommandText(strCmd);  
         cmd2.setPrepared(true);  
  
         // Set a timer, then execute the unprepared  
         // command 20 times.  
         timeStart = System.currentTimeMillis();  
         for ( intLoop = 0; intLoop < 20; intLoop++)  
            cmd1.execute();  
         timeEnd = System.currentTimeMillis();  
         timeNotPrepared =(float)(timeEnd - timeStart)/1000f;  
  
         // Reset the timer, then execute the prepared  
         // command 20 times.  
         timeStart = System.currentTimeMillis();  
         for ( intLoop = 0; intLoop < 20; intLoop++)  
            cmd2.execute();  
         timeEnd = System.currentTimeMillis();  
         timePrepared =(float)(timeEnd - timeStart)/1000f;  
  
         // Display performance results.  
         System.out.println("\nPerformance Results:");  
         System.out.println("\n\tNot Prepared: " + timeNotPrepared +   
            " seconds");  
         System.out.println("\n\tPrepared: " + timePrepared +   
            " seconds");  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Connection, check for null pointer first.  
         if (cnConn1!= null)  
         {  
            PrintProviderError(cnConn1);  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
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
// EndPreparedJ  
  
```  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)   
 [Prepared Property (ADO)](../../../ado/reference/ado-api/prepared-property-ado.md)