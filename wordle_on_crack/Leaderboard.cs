using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace wordle_on_crack;
public class Player
{
    public string Username { get; set; }
    public int Highscore{get;set;}
}
public class Leaderboard
{
    Player player = new Player();
    string LbFile = "./../../../leaderboard.xlsx";
    public void ShowLeaderboard()
    {
        List<Player> players = new List<Player>();
        List<Player> sortedPlayers = new List<Player>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(new FileInfo(LbFile)))
        {
            var worksheet = package.Workbook.Worksheets[0];

            //indexes -->last used row
            int lastrow = worksheet.Dimension.End.Row;
            
            //sorting the PlayerList/Leaderboard to display
            for (int row = 2; row <= lastrow; row++)
            {
                Player p = new Player();
                p.Username = worksheet.Cells[row, 1].Text;
                p.Highscore = int.Parse(worksheet.Cells[row, 2].Text);
                players.Add(p);
            }
            //used GPT
            //How to use Lambda Functions:
            //Apparently uses LINQ (parameter => execution/code)
            sortedPlayers = players.OrderByDescending(p => p.Highscore).ToList();
        }
        Console.WriteLine("################# -Leaderboard - #################\n Player   - -   Score\n");
        foreach (var pl in sortedPlayers)
        {
            Console.WriteLine($" {pl.Username}   - -   {pl.Highscore}");
        }
    }

    public void CreatePlayer(string username)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(new FileInfo(LbFile)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            
            //indexes -->last used row
            int lastrow = worksheet.Dimension.End.Row;
            
            bool found = false;
            for (int row = 2; row <= lastrow; row++)
            {
                var uname = worksheet.Cells[row, 1].Text;
                if (uname == username)
                {
                    found = true;
                }
            }

            if (!found)
            {
                worksheet.Cells[lastrow+1,1].Value = username;
                worksheet.Cells[lastrow+1,2].Value = 0;
                
            }
            // Save to file
            package.SaveAs(new FileInfo(LbFile));
        }
    }
    
    public void UpdateScore(string username)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(new FileInfo(LbFile)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            
            //indexes -->last used row
            int lastrow = worksheet.Dimension.End.Row;
            
            for (int row = 2; row <= lastrow; row++)
            {
                var uname = worksheet.Cells[row, 1].Text;
                if (uname == username)
                {
                    int currenthighscore = int.Parse(worksheet.Cells[row, 2].Text);
                    worksheet.Cells[row,2].Value = currenthighscore +1;
                }
            }
            // Save to file
            package.SaveAs(new FileInfo(LbFile));
        }
    }
    public int GetHighscore(string username)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        int currenthighscore =0;
        using (var package = new ExcelPackage(new FileInfo(LbFile)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            
            //indexes -->last used row
            int lastrow = worksheet.Dimension.End.Row;
            for (int row = 2; row <= lastrow; row++)
            {
                var uname = worksheet.Cells[row, 1].Text;
                if (uname == username)
                {
                    currenthighscore = int.Parse(worksheet.Cells[row, 2].Text);
                    break;
                }
            }
            // Save to file
            package.SaveAs(new FileInfo(LbFile));
        }
        return currenthighscore;
    }
    
}