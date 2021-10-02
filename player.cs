using System;

namespace asciiadventure {
    class Player : MovingGameObject {
        private Boolean hasWeapon = false;
        private int numMine = 0;
        private int treasures = 0;
        
        public Player(int row, int col, Screen screen, string name) : base(row, col, "@", screen) {
            Name = name;
        }
        public string Name {
            get;
            protected set;
        }
        public override Boolean IsPassable(){
            return true;
        }

        public Boolean HasWeapon { // used to check if player has picked up the weapon or not
            get { return this.hasWeapon; }
            set { this.hasWeapon = value; }
        }

        public int NumMine {
            get { return this.numMine; }
            set { this.numMine = value; }
        }

        public int NumTreasures {
            get { return this.treasures; }
            set { this.treasures = value; }
        }

        public String Action(int deltaRow, int deltaCol){
            int newRow = Row + deltaRow;
            int newCol = Col + deltaCol;
            if (!Screen.IsInBounds(newRow, newCol)){
                return "nope";
            }
            GameObject other = Screen[newRow, newCol];
            if (other == null){
                return "negative";
            }
            // TODO: Interact with the object
            if (other is Treasure){
                other.Delete();
                return "Yay, we got the treasure!";
            }
            return "ouch";
        }

        public string Teleport(int newRow, int newCol) {
            
            int originalRow = Row;
            int originalCol = Col;
            // change position of player to the other teleporter
            Row = newRow;
            Col = newCol;
            Screen[originalRow, originalCol] = null;
            Screen[Row, Col] = this;
            return "";
        }
    }
}