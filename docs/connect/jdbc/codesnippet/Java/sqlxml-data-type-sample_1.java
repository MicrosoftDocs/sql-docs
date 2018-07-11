import java.sql.*;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;

import javax.xml.transform.sax.SAXSource;
import javax.xml.transform.sax.SAXResult;
import javax.xml.transform.sax.SAXTransformerFactory;

import org.xml.sax.*;

public class sqlxmlExample {

   public static void main(String[] args) {
	   
      // Create a variable for the connection string.
      String connectionUrl = "jdbc:sqlserver://localhost:1433;" +
            "databaseName=AdventureWorks;integratedSecurity=true;";

      // Declare the JDBC objects.
      Connection con = null;
      Statement stmt = null;
      ResultSet rs = null;
      
      try {
         // Establish the connection.
         Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
         con = DriverManager.getConnection(connectionUrl);

         // Create initial sample data.
         createSampleTables(con);
         
         // The showGetters method demonstrates how to parse the data in the 
         // SQLXML object by using the SAX, ContentHandler and XMLReader.  
         showGetters(con);            

         // The showSetters method demonstrates how to set the xml column 
         // by using the SAX, ContentHandler, and ResultSet.
         showSetters(con);   
         
         // The showTransformer method demonstrates how to get an XML data 
         // from one table and insert that XML data to another table 
         // by using the SAX and the Transformer. 
         showTransformer(con);        
      }
      // Handle any errors that may have occurred.
      catch (Exception e) {
         e.printStackTrace();
      }
      finally {
         if (rs != null) try { rs.close(); } catch(Exception e) {}
         if (stmt != null) try { stmt.close(); } catch(Exception e) {}
         if (con != null) try { con.close(); } catch(Exception e) {}
      }
   }
  
   private static void showGetters(Connection con) {
	   
      try {
          // Create an instance of the custom content handler.   
          ExampleContentHandler myHandler = new ExampleContentHandler();
		   
          // Create and execute an SQL statement that returns a
          // set of data.       
          String SQL = "SELECT * FROM TestTable1";
          Statement stmt = con.createStatement();
          ResultSet rs = stmt.executeQuery(SQL);
      
          rs.next();
        
          SQLXML xmlSource = rs.getSQLXML("Col3");
        
          // Send SAX events to the custom content handler.
          SAXSource sxSource =  xmlSource.getSource(SAXSource.class);
          XMLReader xmlReader = sxSource.getXMLReader();
          xmlReader.setContentHandler(myHandler);
          
	      System.out.println("showGetters method: Parse an XML data in TestTable1 => ");
          xmlReader.parse(sxSource.getInputSource());
        
      } catch (Exception e) {
          e.printStackTrace();
      }
   }
   
   private static void showSetters(Connection con) {

      try {
         // Create and execute an SQL statement, retrieving an updatable result set.
         String SQL = "SELECT * FROM TestTable1;";
         Statement stmt = con.createStatement(ResultSet.TYPE_FORWARD_ONLY, ResultSet.CONCUR_UPDATABLE);
         ResultSet rs = stmt.executeQuery(SQL);
		   
         // Create an empty SQLXML object.
         SQLXML sqlxml = con.createSQLXML();
 
         // Set the result value from SAX events.
         SAXResult sxResult = sqlxml.setResult(SAXResult.class);
         ContentHandler myHandler = sxResult.getHandler();

         // Set the XML elements and attributes into the result.
         myHandler.startDocument();
         myHandler.startElement(null, "contact", "contact", null);
         myHandler.startElement(null, "name", "name", null);
         myHandler.endElement(null, "name", "name");
         myHandler.startElement(null, "phone", "phone", null);
         myHandler.endElement(null, "phone", "phone");		   
         myHandler.endElement(null, "contact", "contact");
         myHandler.endDocument();

         // Update the data in the result set.
         rs.moveToInsertRow();
         rs.updateString("Col2", "C");
         rs.updateSQLXML("Col3", sqlxml);
         rs.insertRow();
		   
         // Display the data. 
         System.out.println("showSetters method: Display data in TestTable1 => ");
         while (rs.next()) {
           System.out.println(rs.getString("Col1") + " : " + rs.getString("Col2"));
           SQLXML xml = rs.getSQLXML("Col3");              
           System.out.println("XML column : " + xml.getString());
         }
      } catch (Exception e) {
         e.printStackTrace();
      }
   }

   private static void showTransformer(Connection con) {

      try {
          // Create and execute an SQL statement that returns a
          // set of data.       
          String SQL = "SELECT * FROM TestTable1";
          Statement stmt = con.createStatement();
          ResultSet rs = stmt.executeQuery(SQL);
	       
          rs.next();
	        
          // Get the value of the source SQLXML object from the database.
          SQLXML xmlSource = rs.getSQLXML("Col3");
	        
          // Get a Source to read the XML data. 
          SAXSource sxSource =  xmlSource.getSource(SAXSource.class);
	        
         // Create a destination SQLXML object without any data.
         SQLXML xmlDest = con.createSQLXML();

         // Get a Result to write the XML data.
         SAXResult sxResult = xmlDest.setResult(SAXResult.class);
	        
         // Transform the Source to a Result by using the identity transform.
         SAXTransformerFactory stf = (SAXTransformerFactory) TransformerFactory.newInstance();
         Transformer identity = stf.newTransformer();
         identity.transform(sxSource, sxResult);

         // Insert the destination SQLXML object into the database.
         PreparedStatement psmt = 
           con.prepareStatement(
              "INSERT INTO TestTable2" + " (Col2, Col3, Col4, Col5) VALUES (?, ?, ?, ?)");
         psmt.setString(1, "A");
         psmt.setString(2, "Test data");
         psmt.setInt(3, 123);
         psmt.setSQLXML(4, xmlDest);
         psmt.execute();
	        
         // Execute the query and display the data.	
         SQL = "SELECT * FROM TestTable2";
         stmt = con.createStatement();
         rs = stmt.executeQuery(SQL);

         System.out.println("showTransformer method : Display data in TestTable2 => ");
         while (rs.next()) {
            System.out.println(rs.getString("Col1") + " : " + rs.getString("Col2"));
            System.out.println(rs.getString("Col3") + " : " + rs.getInt("Col4"));
	          
            SQLXML xml = rs.getSQLXML("Col5");              
            System.out.println("XML column : " + xml.getString());
         }
     } catch (Exception e) {
        e.printStackTrace();
     }
   }
   
   private static void createSampleTables(Connection con) {

      try {
	  Statement stmt = con.createStatement();
		  
          // Drop the tables.
	  stmt.executeUpdate("if exists (select * from sys.objects where name = 'TestTable1')" +
                "drop table TestTable1" );

          stmt.executeUpdate("if exists (select * from sys.objects where name = 'TestTable2')" +
                "drop table TestTable2" );		  

          // Create empty tables.
          stmt.execute("CREATE TABLE TestTable1 (Col1 int IDENTITY, Col2 char, Col3 xml)");
          stmt.execute("CREATE TABLE TestTable2 (Col1 int IDENTITY, Col2 char, Col3 varchar(50), Col4 int, Col5 xml)");

          // Insert two rows to the TestTable1.
         String row1 = "<contact><name>Contact Name 1</name><phone>XXX-XXX-XXXX</phone></contact>";
         String row2 = "<contact><name>Contact Name 2</name><phone>YYY-YYY-YYYY</phone></contact>";

         stmt.executeUpdate("insert into TestTable1" + " (Col2, Col3) values('A', '" + row1 +"')");
         stmt.executeUpdate("insert into TestTable1" + " (Col2, Col3) values('B', '" + row2 +"')");
	      
       } catch (Exception e) {
          e.printStackTrace();
       }
   }
}

class ExampleContentHandler implements ContentHandler {

  public void startElement(String namespaceURI, String localName, String qName, Attributes atts) 
  throws SAXException {
	  System.out.println("startElement method: localName => " + localName);
  }
  public void characters(char[] text, int start, int length) throws SAXException {
  	  System.out.println("characters method");  
  }
  public void endElement(String namespaceURI, String localName, String qName) throws SAXException {
      System.out.println("endElement method: localName => " + localName);		  
  } 
  public void setDocumentLocator(Locator locator) {
	  System.out.println("setDocumentLocator method");
  }
  public void startDocument() throws SAXException {
	  System.out.println("startDocument method");
  }
  public void endDocument() throws SAXException {
	  System.out.println("endDocument method");
  }
  public void startPrefixMapping(String prefix, String uri) throws SAXException {
	  System.out.println("startPrefixMapping method: prefix => " + prefix);
  }
  public void endPrefixMapping(String prefix) throws SAXException {
	  System.out.println("endPrefixMapping method: prefix => " + prefix);
  }
  public void skippedEntity(String name) throws SAXException {
	  System.out.println("skippedEntity method: name => " + name);
  }  
  public void ignorableWhitespace(char[] text, int start, int length) throws SAXException {
	  System.out.println("ignorableWhiteSpace method");
  }
  public void processingInstruction(String target, String data) throws SAXException {
	  System.out.println("processingInstruction method: target => " + target);
  }
}