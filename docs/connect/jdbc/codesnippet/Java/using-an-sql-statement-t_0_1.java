public static void executeUpdateStatement(Connection con) {
   try {
      String SQL = "CREATE TABLE TestTable (Col1 int IDENTITY, Col2 varchar(50), Col3 int)";
      Statement stmt = con.createStatement();
      int count = stmt.executeUpdate(SQL);
      stmt.close();

      System.out.println("ROWS AFFECTED: " + count);
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}