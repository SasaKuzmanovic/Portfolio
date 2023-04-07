import discord
import commands
import pygame
from pynput.keyboard import Key, Controller


intents = discord.Intents.all()
client = discord.Client(command_prefix='!', intents=intents)

myGuild = ""
role = ""

saveChannel = ""

neCekam = False

joysticks = {}
keyboard = Controller()

memberList = []

@client.event
async def on_ready():
    pygame.init()

    pygame.joystick.init()
    if pygame.joystick.get_count() > 0:
        print(f'Initan sam')

    print(f'PYGAME initialised')

    print(f'Choose if you are a streamer or a viewer!')
    print(f'Select [1] if you are a streamer')
    print(f'Or select [2] if you are a viewer')

    


    value = input('Type 1 or 2:')

    if value == '2':
        waitingForInput()
    else:
        myGuild = client.get_guild(1047959171825405972)
        role = discord.utils.get(myGuild.roles, id=1047961554680823898)

        print(f'We have logged in as {client.user}!')  ## Prints the user name of the Bot when it connects
        print(f'Guild: {myGuild}')  # Prints the Server name where it is currently operating
        print(f'Role for sharing gameplay: {role}') # Displays the role that a viewer has to have

    

def waitingForInput():
    done = False
    while not done:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                done = True  # Flag that we are done so we exit this loop.

            if event.type == pygame.JOYBUTTONDOWN:
                print("Joystick button pressed.")
                if event.button == 0: ## This is X
                    # To be added   1. Add message sending to channels
                    #               2. Triggering a function when a button is pressed and the message is sent to discord. Try to trigger the function before the message is sent to reduce latency
                    joystick = joysticks[event.instance_id]
                    keyboard.press('w')
                    keyboard.press(Key.enter)
                    if joystick.rumble(0, 0.7, 500):
                        print(f"Rumble effect played on joystick {event.instance_id}")
                if event.button == 1:
                    joystick = joysticks[event.instance_id]
                    keyboard.press('y')
                    keyboard.press(Key.enter)
                if event.button == 13:
                    joystick = joysticks[event.instance_id]
                    keyboard.press('a')
                    keyboard.press(Key.enter)
                if event.button == 14:
                    joystick = joysticks[event.instance_id]
                    keyboard.press('d')
                    keyboard.press(Key.enter)

            if event.type == pygame.JOYBUTTONUP:
                print("Joystick button released.")

            # Handle hotplugging
            if event.type == pygame.JOYDEVICEADDED:
                joy = pygame.joystick.Joystick(event.device_index)
                joysticks[joy.get_instance_id()] = joy
                print(f"Joystick {joy.get_instance_id()} connencted")

            if event.type == pygame.JOYDEVICEREMOVED:
                del joysticks[event.instance_id]
                print(f"Joystick {event.instance_id} disconnected")


@client.event
async def on_message(message):
    user = message.author

    forward = False
    backwards = False
    left = False
    right = False   
    saveChannel = message.channel

    myGuild = client.get_guild(1047959171825405972)
    role = discord.utils.get(myGuild.roles, id=1047961554680823898)
    for member in role.members:
        memberList.append(member)
    
    if message.author == client.user:
        return

    if user in memberList:
        print(f'User: {user} has the role: {role}')

        print(message.content)
        commands.checkForContents(message)

        if message.author == client.user:
            return
        if message.content.startswith('Hello'):
            await message.channel.send('Sup!')


        if message.content.startswith('Forward'):
            keyboard.press('a')
            commands.forward()
            await message.channel.send('Forward Control Toggled!')

        if message.content.startswith('Backwards'):
            commands.backwards()   
            await message.channel.send('Backwards Control Toggled!')
    else:
        await message.channel.send('You do not have the permission to control the game!')
        

client.run("MTA0Nzk1OTgwMjA4MjUwODg2MA.GrAq9T.Zw3XsSIcGbsvHKuJHHYLoMZYoCOfw-j8LJZNp8")


## Try getting input working in the game