using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/*
 Cool ass stuff people could implement:
 > jumping
 > attacking
 > randomly moving monsters
 > smarter moving monsters
*/
namespace asciiadventure {
    public class Game {
        private Random random = new Random();
        private static Boolean Eq(char c1, char c2){
            return c1.ToString().Equals(c2.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        private static string Menu() {
            return "WASD to move\nIJKL to jump\nPick up W for a Weapon\nEnter command: ";
        }

        private static void PrintScreen(Screen screen, string message, string menu) {
            Console.Clear();
            Console.WriteLine(screen);
            Console.WriteLine($"\n{message}");
            Console.WriteLine($"\n{menu}");
        }
        public void Run() {
            Console.ForegroundColor = ConsoleColor.Green;

            Screen screen = new Screen(10, 20);
            // add some walls
            for (int i=0; i < 3; i++){
                new Wall(1, 4 + i, screen);
            }
            for (int i=0; i < 4; i++){
                new Wall(3 + i, 6, screen);
            }
            for (int i=0; i < 6; i++){
                new Wall(7, 11 + i, screen);
            }
            
            // add a player
            Player player = new Player(0, 0, screen, "Zelda");
            
            // add treasures
            Treasure treasure1 = new Treasure(6, 2, screen);
            Treasure treasure2 = new Treasure(2, 15, screen);
            Treasure treasure3 = new Treasure(5, 12, screen);

            // add teleporters
            Teleporter teleporter1 = new Teleporter(2, 9, screen);
            Teleporter teleporter2 = new Teleporter(8, 2, screen);

            // add a weapon
            Weapon weapon = new Weapon(0, 6, screen);

            // add mines
            //Mine mine1 = new Mine(8, 6, screen);

            // add mobs
            List<Mob> mobs = new List<Mob>();
            mobs.Add(new Mob(9, 9, screen));
            
            // initially print the game board
            PrintScreen(screen, "Welcome!", Menu());
            
            Boolean gameOver = false;
            
            while (!gameOver) {
                char input = Console.ReadKey(true).KeyChar;

                String message = "";

                if (Eq(input, 'q')) {
                    break;
                } else if (Eq(input, 'm') && (player.NumMine > 0)) { // if player has weapon, you can provide direction to place mines
                    message += "PLAYER PLACING MINE\nPress WASD for direction";
                    PrintScreen(screen, message, Menu());
                    char input2 = Console.ReadKey(true).KeyChar;
                    if (Eq(input2, 'w')) {
                        Mine m = new Mine(player.Row-1, player.Col, screen);
                    } else if (Eq(input2, 's')) {
                        Mine m = new Mine(player.Row+1, player.Col, screen);
                    } else if (Eq(input2, 'a')) {
                        Mine m = new Mine(player.Row, player.Col-1, screen);
                    } else if (Eq(input2, 'd')) {
                        Mine m = new Mine(player.Row, player.Col+1, screen);
                    }
                    player.NumMine--;
                } else if (Eq(input, 'm') && (player.NumMine <= 0)) {
                    message += "You have no Weapons\n";
                } else if (Eq(input, 'w')) {  // sorry for all the lines of code, I couldn't find a more efficient way to write this
                    if(player.Row==teleporter1.Row+1 && player.Col==teleporter1.Col) player.Teleport(teleporter2.Row-1,teleporter2.Col);
                    else if(player.Row==teleporter2.Row+1 && player.Col==teleporter2.Col) player.Teleport(teleporter1.Row-1,teleporter1.Col);
                    else if(screen[player.Row-1, player.Col] is Weapon) {player.Move(-1, 0); player.NumMine = 3; message += "You got 3 Mines!\nPress 'M' to place a Mine";}
                    else if(screen[player.Row-1, player.Col] is Treasure) {player.Move(-1, 0); message += "You got a Treasure!\n"; player.NumTreasures++; if(player.NumTreasures==3) {message += "You got all the Treasures! Game Over"; gameOver = true;}}
                    else player.Move(-1, 0);
                } else if (Eq(input, 's')) {  // these lines allow player to do actions like teleporting, picking up weapons and treasures
                    if(player.Row==teleporter1.Row-1 && player.Col==teleporter1.Col) player.Teleport(teleporter2.Row+1,teleporter2.Col);
                    else if(player.Row==teleporter2.Row-1 && player.Col==teleporter2.Col) player.Teleport(teleporter1.Row+1,teleporter1.Col);
                    else if(screen[player.Row+1, player.Col] is Weapon) {player.Move(1, 0); player.NumMine = 3; message += "You got 3 Mines!\nPress 'M' to place a Mine";}
                    else if(screen[player.Row+1, player.Col] is Treasure) {player.Move(1, 0); message += "You got a Treasure!\n"; player.NumTreasures++; if(player.NumTreasures==3) {message += "You got all the Treasures! Game Over"; gameOver = true;}}
                    else player.Move(1, 0);
                } else if (Eq(input, 'a')) {
                    if(player.Row==teleporter1.Row && player.Col-1==teleporter1.Col) player.Teleport(teleporter2.Row,teleporter2.Col-1);
                    else if(player.Row==teleporter2.Row && player.Col-1==teleporter2.Col) player.Teleport(teleporter1.Row,teleporter1.Col-1);
                    else if(screen[player.Row, player.Col-1] is Weapon) {player.Move(0, -1); player.NumMine = 3; message += "You got 3 Mines!\nPress 'M' to place a Mine";}
                    else if(screen[player.Row, player.Col-1] is Treasure) {player.Move(0, -1); message += "You got a Treasure!\n"; player.NumTreasures++; if(player.NumTreasures==3) {message += "You got all the Treasures! Game Over"; gameOver = true;}}
                    else player.Move(0, -1);
                } else if (Eq(input, 'd')) {
                    if(player.Row==teleporter1.Row && player.Col+1==teleporter1.Col) player.Teleport(teleporter2.Row,teleporter2.Col+1);
                    else if(player.Row==teleporter2.Row && player.Col+1==teleporter2.Col) player.Teleport(teleporter1.Row,teleporter1.Col+1);
                    else if(screen[player.Row, player.Col+1] is Weapon) {player.Move(0, 1); player.NumMine = 3; message += "You got 3 Mines!\nPress 'M' to place a Mine";}
                    else if(screen[player.Row, player.Col+1] is Treasure) {player.Move(0, 1); message += "You got a Treasure!\n"; player.NumTreasures++; if(player.NumTreasures==3) {message += "You got all the Treasures! Game Over"; gameOver = true;}}
                    else player.Move(0, 1);
                } else if (Eq(input, 'i')) { // jumping 2 steps
                    player.Move(-2, 0);
                } else if (Eq(input, 'k')) {
                    player.Move(2, 0);
                } else if (Eq(input, 'j')) {
                    player.Move(0, -2);
                } else if (Eq(input, 'l')) {
                    player.Move(0, 2);
                } else if (Eq(input, 'v')) {
                    // TODO: handle inventory
                    message = "You have nothing\n";
                } else {
                    message = $"Unknown command: {input}";
                }

                // OK, now move the mobs
                foreach (Mob mob in mobs){
                    // TODO: Make mobs smarter, so they jump on the player, if it's possible to do so
                    List<Tuple<int, int>> moves = screen.GetLegalMoves(mob.Row, mob.Col);
                    if (moves.Count == 0){
                        continue;
                    }
                    // mobs move randomly
                    var (deltaRow, deltaCol) = moves[random.Next(moves.Count)];
                    
                    if (screen[mob.Row + deltaRow, mob.Col + deltaCol] is Player){
                        // the mob got the player!
                        mob.Token = "*";
                        message += "A MOB GOT YOU! GAME OVER\n";
                        gameOver = true;
                    }
                    if (screen[mob.Row + deltaRow, mob.Col + deltaCol] is Mine){ // mob dies after stepping on mine
                        // the mob stepped on a mine!
                        mob.Token = "*";
                        message += "THE MOB IS DEAD! GAME OVER\n";
                        gameOver = true;
                    }
                    mob.Move(deltaRow, deltaCol);
                }

                PrintScreen(screen, message, Menu());
            }
        }

        public static void Main(string[] args){
            Game game = new Game();
            game.Run();
        }
    }
}