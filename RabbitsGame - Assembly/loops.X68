*-------------------------------------------------------
* STARTING MEMORY ADDRESS FOR THE PROGRAMME $1000
*-------------------------------------------------------
    
    ORG $1000

*-------------------------------------------------------
*VALIDATION VALUES TO BE USED, MODIFY AS NEEDED
*ADD ADDITIONAL VALIDATION VALUES AS REQUIRED
*-------------------------------------------------------
EXIT        EQU 0      USED TO EXIT ASSEMBLY PROGRAM
MIN_MONEY EQU 1      MIN NUMBER OF MONEY
MAX_MONEY EQU 20      MAX NUMBER OF MONEY
WIN_POINT   EQU 5      POINTS ACCUMILATED ON WIN
LOSE_POINT  EQU 8      POINTS DEDUCTED ON A LOSS

MINE_LOC    EQU 100    USED BELOW FOR SOME SIMPLE COLLISION DETECTION USING CMP
                       * EXAMPLE FOR A HIT
RABBIT_DAMAGE   EQU 2   Damage for ze rabbit
LIGHT_HIT   EQU 2 Damage that player can use
NORMAL_HIT  EQU 4 Damage that player can use
HEAVY_HIT   EQU 8 Damage that player can use

PLAYER_HEALTH EQU 16 Player health
ENEMY_HEALTH EQU 8 Enemy health



*START OF GAME
START:
    MOVE.B  #PLAYER_HEALTH,$4000 PLAYER HEALTH LOCATION $4000
    LEA     $4000,A3   ASSIGN ADDRESS A3 TO THAT MEMORY LOCATION
    
    MOVE.B  #ENEMY_HEALTH,$4010 PLAYER HEALTH LOCATION $4010
    LEA     $4010,A4


    BSR     WELCOME    BRANCH TO THE WELCOME SUBROUTINE
    BSR     INPUT      BRANCH TO THE INPUT SUBROUTINE
    BSR     GAME       BRANCH TO THE GAME SUBROUTINE
*GAME LOOP
    ORG     $3000      THE REST OF THE PROGRAM IS TO BE LOCATED FROM 3000 ONWARDS

*-------------------------------------------------------
*-------------------GAME SUBROUTINE---------------------
*-------------------------------------------------------
GAME:
    BSR     GAMELOOP   BRANCH TO GAMELOOP SUBROUTINE
    RTS                RETURN FROM GAME: SUBROUTINE
          
END:
    SIMHALT

*-------------------------------------------------------
*-------------------WELCOME SUBROUTINE------------------
*-------------------------------------------------------
WELCOME:
    BSR     ENDL            BRANCH TO ENDL SUBROUTINE
    LEA     WELCOME_MSG,A1  ASSIGN MESSAGE TO ADDRESS REGISTER A1
    MOVE.B  #14,D0          MOVE LITERAL 14 TO DO
    TRAP    #15             TRAP AND INTERPRET VALUE IN D0
    BSR     ENDL            BRANCH TO ENDL SUBROUTINE
    RTS                     RETURN FROM WELCOME: SUBROUTINE

*-------------------------------------------------------
*---------GAMEPLAY INPUT VALUES SUBROUTINE--------------
*-------------------------------------------------------    
INPUT:
    BSR     MONEY         BRANCH TO POTION INPUT SUBROUTINE
    BSR     WEAPONS         BRANCH TO WEAPONSS INPUT SUBROUTINE
    RTS

*-------------------------------------------------------
*----------------GAMELOOP (MAIN LOOP)-------------------
*------------------------------------------------------- 
GAMELOOP:
    BSR     UPDATE          BRANCH TO UPDATE GAME SUBROUTINE 
   *- BSR     CLEAR_SCREEN    CLEARS THE SCREEN         
    BSR     DRAW            BRANCH TO DRAW SCREEN SUBROUTINE
   *- BSR     CLEAR_SCREEN    CLEARS THE SCREEN
    BSR     GAMEPLAY        BRANCH TO GAMEPLAY SUBROUTINE
    *-BSR     CLEAR_SCREEN    CLEARS THE SCREEN
    BSR     HUD             BRANCH TO DISPLAY HUD SUBROUTINE
   *- BSR     CLEAR_SCREEN    CLEARS THE SCREEN
    BSR     REPLAY          BRANCH TO REPLAY GAME SUBROUTINE
    *-BSR     CLEAR_SCREEN    CLEARS THE SCREEN
    RTS                     RETURN FROM GAMELOOP: SUBROUTINE

*-------------------------------------------------------
*----------------UPDATE QUEST PROGRESS------------------
*  COMPLETE QUEST
*------------------------------------------------------- 
UPDATE:
    BSR     ENDL            PRINT A CR AND LF
    BSR     DECORATE        DECORATE WITH DOTS USING A LOOP
    LEA     UPDATE_MSG,A1   
    MOVE.B  #14,D0
    TRAP    #15
    BSR     DECORATE
    RTS
*-------------------------------------------------------
*-----------------DRAW QUEST UPDATES--------------------
* DRAW THE GAME PROGRESS INFORMATION, STATUS REGARDING
* QUEST
*------------------------------------------------------- 
DRAW:
    BSR     ENDL
    BSR     DECORATE
    LEA     DRAW_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    BSR     DECORATE
    RTS

*-------------------------------------------------------
*--------------------MONEY INVENTORY---------------------
* AMOUNT OF MONEY TO BE USED IN A SHOP
*------------------------------------------------------- 
MONEY:
    BSR     ENDL
    BSR     DECORATE
    LEA     MONEY,A1
    MOVE.B  #14,D0
    TRAP    #15
    BSR     DECORATE
    RTS

*-------------------------------------------------------
*-------------------------WEAPONS-----------------------
* NUMBER OF WEAPONS
*-------------------------------------------------------   
WEAPONS:
    BSR     ENDL
    BSR     DECORATE
    LEA     WEAPONS_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    BSR     DECORATE
    RTS

*-------------------------------------------------------
*---GAME PLAY (QUEST PROGRESS)--------------------------
*------------------------------------------------------- 
GAMEPLAY:
    BSR     ENDL
    BSR     DECORATE
    LEA     GAMEPLAY_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    BSR     DECORATE
    BSR     TUTORIAL
    RTS
    
*-------------------------------------------------------
*----------------TUTORIAL BRANCH------------------------
*------------------------------------------------------- 
TUTORIAL:
    BSR     CLEAR_SCREEN
    BSR     ENDL
    BSR     DECORATE
    LEA     TUTORIAL1_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    LEA     TUTORIAL2_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    LEA     TUTORIAL3_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15

    BSR     DECORATE
    BSR     LORE
    RTS
    
*-------------------------------------------------------
*----------------LORE SUBROUTINE------------------------
*------------------------------------------------------- 
LORE:
    BSR     CLEAR_SCREEN
    BSR     ENDL
    BSR     DECORATE
    LEA     LORE1_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    LEA     LORE2_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    BSR     DECORATE
    BSR     COMBAT_START
    RTS

*-------------------------------------------------------
*-------------COMBAT BEGINNING SUBROUTINE---------------
*------------------------------------------------------- 
COMBAT_START:
    BSR     ENDL
    BSR     DECORATE
    LEA     COMBAT_START1_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    MOVE.B #10,D1 
    MOVE #14,D0 
    TRAP #15 
    
    MOVE #3,D0 
    TRAP #15
    
    BSR     DECORATE
    BSR     PLAYER
    RTS

*-------------------------------------------------------
*------------------PLAYER SUBROUTINE--------------------
*------------------------------------------------------- 
PLAYER:
    *-BSR     CLEAR_SCREEN
    BSR     ENDL
    BSR     DECORATE
    LEA     PLAYER_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    
    MOVE #3,D0 
    TRAP #15
    
    BSR     DECORATE
    BSR     ATTACK_USE
    RTS

*-------------------------------------------------------
*----------------ATTACK USE SUBROUTINE------------------
*------------------------------------------------------- 
ATTACK_USE:
    BSR     CLEAR_SCREEN
    BSR     ENDL
    BSR     DECORATE
    
    LEA     ATTACK_USE_MSG1,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    
    BSR     DECORATE
    BSR     COMBAT
    RTS


*-------------------------------------------------------
*------------------COMBAT SUBROUTINE--------------------
*------------------------------------------------------- 
COMBAT:
    *-BSR     CLEAR_SCREEN
    BSR     ENDL
    BSR     DECORATE
    
    LEA     COMBAT_MSG1,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    LEA     COMBAT_MSG2,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    LEA     $4000,A3
    MOVE.B  $4000,D0
    SUBI.B  #RABBIT_DAMAGE,D0
    MOVE.B  D0,$4000
    
    LEA     COMBAT_MSG3,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  #4,D0
    TRAP    #15
    
    MOVE.B  #4,D0
    TRAP    #15
    MOVE.B  D0, $4005
    LEA     $4005,A6
    
    CMP     #1,D1
    BEQ     LIGHT_ATTACK
    
    CMP     #2,D1
    BEQ     NORMAL_ATTACK
    
    CMP     #3,D1
    BEQ     HEAVY_ATTACK
    
    
    BSR     DECORATE
    BSR     LOSE_SCREEN
    RTS
    
*-------------------------------------------------------
*----------------LIGHT ATTACK SUBROUTINE----------------
*-------------------------------------------------------
LIGHT_ATTACK:
    BSR ENDL
    BSR DECORATE
    
    LEA LIGHT_ATTACK_MSG,A1
    MOVE.B #14,D0
    TRAP #15
    
    LEA $4010,A4
    MOVE.B $4010,D0
    
    SUBI.B  #LIGHT_HIT,D0
    MOVE.B  D0,$4010
    
    CMP     #0,D0
    BEQ     WIN_SCREEN
    
    BSR     COMBAT
    
*-------------------------------------------------------
*----------------NORMAL ATTACK SUBROUTINE----------------
*-------------------------------------------------------
NORMAL_ATTACK:
    BSR ENDL
    BSR DECORATE
    
    LEA NORMAL_ATTACK_MSG,A1
    MOVE.B #14,D0
    TRAP #15
    
    LEA $4010,A4
    MOVE.B $4010,D0
    
    SUBI.B  #NORMAL_HIT,D0
    MOVE.B  D0,$4010
    
    CMP     #0,D0
    BEQ     WIN_SCREEN
    
    BSR     COMBAT
    
*-------------------------------------------------------
*----------------HEAVY ATTACK SUBROUTINE----------------
*-------------------------------------------------------
HEAVY_ATTACK:
    BSR ENDL
    BSR DECORATE
    
    LEA HEAVY_ATTACK_MSG,A1
    MOVE.B #14,D0
    TRAP #15
    
    LEA $4010,A4
    MOVE.B $4010,D0
    
    SUBI.B  #HEAVY_HIT,D0
    MOVE.B  D0,$4010
    
    CMP     #0,D0
    BEQ     WIN_SCREEN
    
    BSR     COMBAT
    
*-------------------------------------------------------
*-----------------LOSE SCREEN SUBROUTINE-----------------
*-------------------------------------------------------
LOSE_SCREEN:    
    BSR ENDL
    BSR DECORATE
    
    LEA LIGHT_ATTACK_MSG,A1
    MOVE.B #14,D0
    TRAP #15
    
    SIMHALT

*-------------------------------------------------------
*-----------------WIN SCREEN SUBROUTINE-----------------
*-------------------------------------------------------
WIN_SCREEN:    
    BSR ENDL
    BSR DECORATE
    
    LEA WIN_MSG,A1
    MOVE.B #14,D0
    TRAP #15
    
    SIMHALT

*-------------------------------------------------------
*-----------------HEADS UP DISPLAY (SCORE)--------------
* RETRIEVES THE SCORE FROM MEMORY LOCATION
*-------------------------------------------------------   
HUD:
    
    BSR     ENDL
    BSR     DECORATE
    LEA     HUD_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    MOVE.B  (A3),D1     RETRIEVE THE VALUE A3 POINT TO AND MOVE TO D1
    MOVE.B  #3,D0       MOVE LITERAL 3 TO D0
    TRAP    #15         INTREPRET VALUE IN D0, WHICH 3 WHICH DISPLAYS D1
    BSR     DECORATE
    RTS

*-------------------------------------------------------
*-----------------------BEING ATTACKED------------------
* THIS COULD BE USED FOR COLLISION DETECTION
*-------------------------------------------------------
COLLISION:
    MOVE.B  #MINE_LOC,D1
    CMP     #100,D1 IS( X == 100)?
	BNE     COLLISION_MISS IF X IS EQUAL TO 100, THEN HIT
COLLISION_HIT:
    *HIT
    LEA     HIT_MSG,A1
    MOVE    #14,D0
    TRAP    #15
    RTS
    
COLLISION_MISS:
    *MISS
    LEA     MISS_MSG,A1
    MOVE    #14,D0
    TRAP    #15
    RTS

*-------------------------------------------------------
*--------------------------LOOP-------------------------
*-------------------------------------------------------
LOOP:
    MOVE.B  #5, D3 LOOP COUNTER D3=5
NEXT:
    LEA     LOOP_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
	SUB     #1,D3   DECREMENT LOOP COUNTER
    BNE     NEXT    REPEAT UNTIL D0=0

*-------------------------------------------------------
*------------------SCREEN DECORATION--------------------
*-------------------------------------------------------
DECORATE:
    MOVE.B  #60, D3
    BSR     ENDL
OUT:
    LEA     LOOP_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
	SUB     #1,D3   DECREMENT LOOP COUNTER
    BNE     OUT	    REPEAT UNTIL D0=0
    BSR     ENDL
    RTS
    
CLEAR_SCREEN: 
    MOVE.B  #11,D0      CLEAR SCREEN
    MOVE.W  #$FF00,D1
    TRAP    #15
    RTS
*-------------------------------------------------------
*------------------------REPLAY-------------------------
*-------------------------------------------------------
REPLAY:
    BSR     ENDL
    LEA     REPLAY_MSG,A1
    MOVE.B  #14,D0
    TRAP    #15
    
    MOVE.B  #4,D0
    TRAP    #15

    CMP     #EXIT,D1
    BEQ     END         IF SR Z REGISTER CONTAINS 1 BEQ => BRANCH EQUALS
    BSR     GAMELOOP

ENDL:
    MOVEM.L D0/A1,-(A7)
    MOVE    #14,D0
    LEA     CRLF,A1
    TRAP    #15
    MOVEM.L (A7)+,D0/A1
    RTS
    
*-------------------------------------------------------
*-------------------DATA DELARATIONS--------------------
*-------------------------------------------------------

CRLF:           DC.B    $0D,$0A,0
WELCOME_MSG:    DC.B    '************************************************************'
                DC.B    $0D,$0A
                DC.B    'STRATEGY GAMES SUCH AS ZORK, AVALON, OR RABBITS VS PLUMBERS'
                DC.B    $0D,$0A
                DC.B    '************************************************************'
                DC.B    $0D,$0A,0
MONEY_MSG:    DC.B    'AMOUNT OF MONEY : ',0
WEAPONS_MSG:    DC.B    'EACH QUEST NEED AT LEAST 2 WEAPONS'
                DC.B    $0D,$0A
                DC.B    'MINIMUM REQUIREMENT IS 2 I.E. SWORD X 1 AND SPEER X 1.'
                DC.B    $0D,$0A
                DC.B    'ENTER # OF WEAPONS : ',0
GAMEPLAY_MSG:   DC.B    'WELCOME TO "THE GAME" !',0
TUTORIAL1_MSG   DC.B    'THIS IS A SIMPLE STRATEGY GAME!',0
TUTORIAL2_MSG   DC.B    'GOAL, SURVIVE LONG ENOUGH TO GET STRONGER THAN ENEMY!',0
TUTORIAL3_MSG   DC.B    'EACH TURN YOU EARN MONEY, USE THE MONEY TO BUY ATTACK DAMAGE',0
LORE1_MSG       DC.B    'EVIL ENEMY WANTS TO KILL THE ENTIRE VILLAGE!',0
LORE2_MSG       DC.B    'SAVE THE VILLAGE AND BECOME THEIR HERO!',0
PLAYER_MSG      DC.B    'PLAYER CAN USE 3 ATTACKS, LIGHT, NORMAL and HEAVY ATTACK!',0
COMBAT_START1_MSG   DC.B    'PLAYER HITPOINTS'
COMBAT_START2_MSG   DC.B    ' ',0
COMBAT_MSG1     DC.B    'ENEMY USED AN ATTACK',0
COMBAT_MSG2     DC.B    'ENEMY DEALT 2 DAMAGE',0
COMBAT_MSG3     DC.B    'HOW WILL YOU FIGHT BACK?'
ATTACK_USE_MSG1 DC.B    'ATTACKS ARE USED WITH BUTTONS 1,2,3'
LIGHT_ATTACK_MSG   DC.B    'LIGHT ATTACK USED!',0
NORMAL_ATTACK_MSG  DC.B    'NORMAL ATTACK USED!',0
HEAVY_ATTACK_MSG   DC.B    'HEAVY ATTACK USED!',0
WIN_MSG         DC.B    'YOU WON YOU MOTHER ...',0
LOSE_MSG        DC.B    'YOU LOST YOU NOOB!',0
UPDATE_MSG:     DC.B    'UPDATE GAMEPLAY !',0
DRAW_MSG:       DC.B    'DRAW SCREEN !',0
HIT_MSG:        DC.B    'STRIKE!',0
MISS_MSG:       DC.B    'MISS!',0
LOOP_MSG:       DC.B    '.',0
REPLAY_MSG:     DC.B    'ENTER 0 TO QUIT ANY OTHER NUMBER TO REPLAY : ',0
HUD_MSG:        DC.B    'SCORE : ',0

HEALTH:     DS.W    1   PLAYERS HEALTH
SCORE:      DS.W    1   RESERVE SPACE FOR SCORE

    END START



*~Font name~Courier New~
*~Font size~10~
*~Tab type~1~
*~Tab size~4~
