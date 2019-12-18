using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Board
    {
        private Pawn[,] board;

        private int firstPawns = 12;
        private int secondPawns = 12;

        // set new board
        public Board()
        {
            board = new Pawn[8, 8];
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if ((x + y) % 2 == 1 && x != 3 && x != 4)
                    {
                        if (x < 3) 
                            board[x, y] = new Pawn(false);
                        else if (x > 4) 
                            board[x, y] = new Pawn(true);
                    }
                    else board[x, y] = null;
                }
            }
        }

        // board copy with movement done
        public Board(Board board, Move move)
        {
            this.firstPawns = board.firstPawns;
            this.secondPawns = board.secondPawns;
            this.board = new Pawn[8, 8];
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (board.getBoard()[x, y] != null)
                        this.board[x, y] = new Pawn(board.getBoard()[x, y]);
                    else
                        this.board[x, y] = null;
                }
            }
            makeMove(move);
        }

        // check pawn positions for GUI, 1 & 3 -first player, 2 & 4 -second
        public int[,] checkPawns() {
            int[,] resultBoard = new int[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y] != null)
                    {
                        if (board[x, y].isFirstPlayer())
                        {
                            if (board[x, y].checkIfKing()) 
                                resultBoard[x, y] = 3;
                            else 
                                resultBoard[x, y] = 1;
                        }
                        else
                        {
                            if (board[x, y].checkIfKing()) 
                                resultBoard[x, y] = 4;
                            else 
                                resultBoard[x, y] = 2;
                        }
                    }
                }
            }
            return resultBoard;
		}
			
        // ensure to avoid out-of border movements
        private bool isInBoard(int x, int y)
        {
            return x > -1 && x < 8 && y > -1 && y < 8;
        }

        // check and grand for king if possible
        private void grandForKing(int x, int y)
        {
            if (board[x, y] !=null && board[x, y].isFirstPlayer() && !board[x, y].checkIfKing() && x == 0)
                board[x, y].makeKing();
            if (board[x, y] != null && !board[x, y].isFirstPlayer() && !board[x, y].checkIfKing() && x == 7)
                board[x, y].makeKing();
        }

        // standard board evaluation - 1
        public int evaluate()
        {
            return firstPawns - secondPawns;
        }

        // evaluation heuristic with edges - 2
        public int evalKings()
        {
            int count1 = 0;
            int count2 = 0;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (board[x, y] != null)
                    {
                        if (board[x, y].isFirstPlayer() && board[x,y].checkIfKing() == false) count1+=3;
                        if (board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true) count1+=2;
                        if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == false) count2+=3;
                        if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true) count2 += 2;
                    }
                }
            }
            return count1 - count2;
        }
        // evaluation heuristic with edges - 3
        public int evalEdges()
        {
            int count1 = 0;
            int count2 = 0;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (board[x, y] != null)
                    {
                        if (board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == false)
                        {
                            if (x == 7 || x == 0 || y == 7 || y == 0)
                                count1 += 10;
                            else
                                count1 += 11;
                        }
                        else if (board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true)
                        {
                            if (x == 7 || x == 0 || y == 7 || y == 0)
                                count1 += 12;
                            else
                                count1 += 13;
                        }
                        if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == false)
                        {
                            if (x == 7 || x == 0 || y == 7 || y == 0)
                                count2 += 10;
                            else
                                count2 += 11;
                        }
                        else if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true)
                        {
                            if (x == 7 || x == 0 || y == 7 || y == 0)
                                count2 += 12;
                            else
                                count2 += 13;
                        }
                    }
                }
            }
            return count1 - count2;
        }

        //evaluation with levels - 4
        public int evalLevels()
        {
            int count1 = 0;
            int count2 = 0;
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (board[x, y] != null)
                    {
                        if (board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == false)
                        {
                            if (x==7 || x == 6)
                                count1 += 4;
                            else if (x == 5 || x == 4)
                                count1 += 5;
                            else if (x == 3 || x == 2)
                                count1 += 6;
                            else if (x == 1 || x == 0)
                                count1 += 7;
                        }
                        else if (board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true)
                        {
                            count1 += 8;
                        }
                        else if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == false)
                        {
                            if (x == 0 || x == 1)
                                count2 += 4;
                            else if (x == 2 || x == 3)
                                count2 += 5;
                            else if (x == 4 || x == 5)
                                count2 += 6;
                            else if (x == 6 || x == 7)
                                count2 += 7;
                        }
                        else if (!board[x, y].isFirstPlayer() && board[x, y].checkIfKing() == true)
                        {
                            count2 += 8;
                        }

                    }
                }
            }
            return count1 - count2;
        }

        // 1 -> Player 0 won, 0 -> Player 1 won, -1 -> no end
        public int endOfGame()
        {
            if (firstPawns == 0) 
                return 2;
            if (secondPawns == 0)
                return 1;
            return -1;
        }

        // checks for all first beatings
        public void checkBeatings(bool isFirstPlayer, List<Move> possibleMovements)
        {
            for (int x = 0; x < 8; ++x)
                for (int y = 0; y < 8; ++y)
                    if (board[x, y] != null && board[x, y].isFirstPlayer() == isFirstPlayer)
                    {
                        if (!board[x, y].checkIfKing())
                        {
                            if (isFirstPlayer)
                            {
                                if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer)
                                    multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x - 2, y - 2, x - 1, y - 1), x, y);
                                if (isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer)
                                    multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x - 2, y + 2, x - 1, y + 1), x, y);
                            }
                            else
                            {
                                if (isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer)
                                    multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x + 2, y - 2, x + 1, y - 1), x, y);
                                if (isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                                    multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x + 2, y + 2, x + 1, y + 1), x, y);
                            }
                        }
                        else
                        {
                            if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer)
                                multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x - 2, y - 2, x - 1, y - 1), x, y);
                            if (isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer)
                                multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x - 2, y + 2, x - 1, y + 1), x, y);
                            if (isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer)
                                multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x + 2, y - 2, x + 1, y - 1), x, y);
                            if (isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                                multipleBeatings(isFirstPlayer, possibleMovements, new Move(x, y, x + 2, y + 2, x + 1, y + 1), x, y);
                        }
                    }
        }

        // checks all possibilities of multiple beatings
        private void multipleBeatings(bool isFirstPlayer, List<Move> possibleMovements, Move move, int startX, int startY)
        {
            int x = move.getNewX();
            int y = move.getNewY();
            int beatenX = move.getBeatingX();
            int beatenY = move.getBeatingY();
            Pawn beatenPawn = board[beatenX, beatenY];
            board[move.getBeatingX(), move.getBeatingY()] = null;
            if (!board[startX, startY].checkIfKing())
            {
                if (isFirstPlayer)
                {
                    if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer ||
                            isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer)
                    {
                        if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer)
                        {
                            Move newMovement = new Move(x, y, x - 2, y - 2, x - 1, y - 1, move);
                            move.setNextBeating(newMovement);
                            multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                        }
                        if (isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer)
                        {
                            Move newMovement = new Move(x, y, x - 2, y + 2, x - 1, y + 1, move);
                            move.setNextBeating(newMovement);
                            multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                        }
                    }
                    else
                    {
                        while (move.getPrevBeating() != null) 
                            move = new Move(move.getPrevBeating());
                        possibleMovements.Add(move);
                    }
                }
                else
                {
                    if (isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer ||
                            isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                    {
                        if (isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer)
                        {
                            Move newMovement = new Move(x, y, x + 2, y - 2, x + 1, y - 1, move);
                            move.setNextBeating(newMovement);
                            multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                        }
                        if (isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                        {
                            Move newMovement = new Move(x, y, x + 2, y + 2, x + 1, y + 1, move);
                            move.setNextBeating(newMovement);
                            multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                        }
                    }
                    else
                    {
                        while (move.getPrevBeating() != null) 
                            move = new Move(move.getPrevBeating());
                        possibleMovements.Add(move);
                    }
                }
            }
            else //kings
            {
                if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer ||
                        isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer ||
                        isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer ||
                        isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                {
                    if (isInBoard(x - 2, y - 2) && board[x - 2, y - 2] == null && board[x - 1, y - 1] != null && board[x - 1, y - 1].isFirstPlayer() != isFirstPlayer)
                    {
                        Move newMovement = new Move(x, y, x - 2, y - 2, x - 1, y - 1, move);
                        move.setNextBeating(newMovement);
                        multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                    }
                    if (isInBoard(x - 2, y + 2) && board[x - 2, y + 2] == null && board[x - 1, y + 1] != null && board[x - 1, y + 1].isFirstPlayer() != isFirstPlayer)
                    {
                        Move newMovement = new Move(x, y, x - 2, y + 2, x - 1, y + 1, move);
                        move.setNextBeating(newMovement);
                        multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                    }
                    if (isInBoard(x + 2, y - 2) && board[x + 2, y - 2] == null && board[x + 1, y - 1] != null && board[x + 1, y - 1].isFirstPlayer() != isFirstPlayer)
                    {
                        Move newMovement = new Move(x, y, x + 2, y - 2, x + 1, y - 1, move);
                        move.setNextBeating(newMovement);
                        multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                    }
                    if (isInBoard(x + 2, y + 2) && board[x + 2, y + 2] == null && board[x + 1, y + 1] != null && board[x + 1, y + 1].isFirstPlayer() != isFirstPlayer)
                    {
                        Move newMovement = new Move(x, y, x + 2, y + 2, x + 1, y + 1, move);
                        move.setNextBeating(newMovement);
                        multipleBeatings(isFirstPlayer, possibleMovements, newMovement, startX, startY);
                    }
                }
                else
                {
                    while (move.getPrevBeating() != null) 
                        move = new Move(move.getPrevBeating());
                    possibleMovements.Add(move);
                }
            }
            board[beatenX, beatenY] = beatenPawn; 
            beatenPawn = null;
        }

        // checks for all not beating movements
        public void getMovements(bool isFirstPlayer, List<Move> possibleMovements)
        {
            for (int x = 0; x < 8; ++x)
                for (int y = 0; y < 8; ++y)
                    if (board[x, y] != null && board[x, y].isFirstPlayer() == isFirstPlayer)
                    {
                        if (!board[x, y].checkIfKing())
                        {
                            if (isFirstPlayer)
                            {
                                if (isInBoard((x - 1), (y - 1)) && board[x - 1, y - 1] == null)
                                    possibleMovements.Add(new Move(x, y, (x - 1), (y - 1)));

                                if (isInBoard((x - 1), (y + 1)) && board[x - 1, y + 1] == null)
                                    possibleMovements.Add(new Move(x, y, (x - 1), (y + 1)));
                            }
                            else
                            {
                                if (isInBoard((x + 1), (y - 1)) && board[x + 1, y - 1] == null)
                                    possibleMovements.Add(new Move(x, y, (x + 1), (y - 1)));
                                if (isInBoard((x + 1), (y + 1)) && board[x + 1, y + 1] == null)
                                    possibleMovements.Add(new Move(x, y, (x + 1), (y + 1)));
                            }
                        }
                        else
                        {
                            if (isInBoard(x - 1, y - 1) && board[x - 1, y - 1] == null)
                                possibleMovements.Add(new Move(x, y, x - 1, y - 1));
                            if (isInBoard(x - 1, y + 1) && board[x - 1, y + 1] == null)
                                possibleMovements.Add(new Move(x, y, x - 1, y + 1));
                            if (isInBoard(x + 1, y - 1) && board[x + 1, y - 1] == null)
                                possibleMovements.Add(new Move(x, y, x + 1, y - 1));
                            if (isInBoard(x + 1, y + 1) && board[x + 1, y + 1] == null)
                                possibleMovements.Add(new Move(x, y, x + 1, y + 1));
                        }
                    }
        }

        // make move
        public void makeMove(Move Move)
        {
            board[Move.getNewX(), Move.getNewY()] = board[Move.getPrevX(), Move.getPrevY()];
            board[Move.getPrevX(), Move.getPrevY()] = null;
            if (Move.getIsBeating())
            {
                if (board[Move.getBeatingX(), Move.getBeatingY()].isFirstPlayer()) 
                    --firstPawns;
                else if (!board[Move.getBeatingX(), Move.getBeatingY()].isFirstPlayer()) 
                    --secondPawns;
                board[Move.getBeatingX(), Move.getBeatingY()] = null;
                if (Move.getNextBeating() != null)
                    makeMove(Move.getNextBeating());
                else 
                    grandForKing(Move.getNewX(), Move.getNewY());
            }
            else grandForKing(Move.getNewX(), Move.getNewY());
        }
             
        public void showBoard() {
		for (int x = 0 ; x < 8 ; ++x) {
			for (int y = 0 ; y < 8 ; ++y) {
				if ((x+y) % 2 == 1) {
					if (board[x,y] != null) {
						if (board[x,y].isFirstPlayer()) {
							if (board[x,y].checkIfKing()) 
                                Console.Write ("3");
							else 
                                Console.Write ("1");
						}
						else {
							if (board[x,y].checkIfKing()) 
                                Console.Write ("4");
							else 
                                Console.Write ("2");
						}
					}
					else Console.Write (" ");
				}
				else Console.Write (" ");
			}
			Console.WriteLine();
		}
		Console.WriteLine(" ");
	}

        public int CountPlr1()
        {
            return firstPawns;
        }
        public int CountPlr2()
        {
            return secondPawns;
        }
        public void SetPlr1(int firstPawns)
        {
            this.firstPawns = firstPawns;
        }
        public void SetPlr2(int secondPawns)
        {
            this.secondPawns = secondPawns;
        }
        public Pawn[,] getBoard()
        {
            return board;
        }
    }
}
