const { Client, Intents } = require('discord.js');
const axios = require('axios');
const client = new Client({ intents: [Intents.FLAGS.GUILDS, Intents.FLAGS.GUILD_MESSAGES] });

const makeRequest = async (url) => await (await axios.get(url));
const makePostRequest = async (url, data) => await (await axios.post(url, data));

client.on('ready', () => {
    if(!commandPrefixes) {
        throw 'Environment variable "BOT_COMMAND_PREFIXES" must be set when using the Discord bot.'
    }

    console.log(`Logged in as ${client.user.tag}!`);
});

client.on("messageCreate", (message) => {
    let commandPrefixes = process.env.BOT_COMMAND_PREFIXES;

    if(!message.content || message.content.length == 0) {
        return;
    }

    const inputCommandPrefix = message.content[0];

    if(inputCommandPrefix && commandPrefixes.includes(inputCommandPrefix)) {
        let command = message.content.slice(1);

        if((command.startsWith("play") || command.startsWith("queue")) && command.includes("spotify")) {
            message.channel.send("Converting...");

            if(!command.includes(' ')) {
                message.channel.send("Please include a URL in your command.");
                return;
            }

            let url = command.split(' ')[1];
            if(url.length == 0) {
                message.channel.send("Please include a URL in your command.");
            }

            const data = {
                url: url
            };
            makePostRequest("http://spotitube_manager", data).then(res => {
                const link = res.data.link;
                const playlist = res.data.playlist;
                let result = ''
                if(link) {
                    result = link;
                }
                else if(playlist) {
                    result = 'playlist ' + playlist;
                }

                result = 'Ready. To play, run command `~play ' + result + '`'
                message.channel.send(result);
            })
            .catch(_ => {
                message.channel.send("Failed to convert.");
            });
        }
        else if(command.startsWith("clearcache")) {
            makeRequest("http://spotitube_manager/clear").then(_ => {
                message.channel.send("Playlist cache cleared!");
            })
            .catch(_ => {
                message.channel.send("Failed to convert.");
            });
        }
    }
});

client.login(process.env.BOT_TOKEN);
