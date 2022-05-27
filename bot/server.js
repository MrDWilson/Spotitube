const { Client, Intents } = require('discord.js');
const axios = require('axios');
const client = new Client({ intents: [Intents.FLAGS.GUILDS, Intents.FLAGS.GUILD_MESSAGES] });

const makeRequest = async (url) => await (await axios.get(url));
const makePostRequest = async (url, data) => await (await axios.post(url, data));

client.on('ready', () => {
  console.log(`Logged in as ${client.user.tag}!`);
});

client.on("messageCreate", (message) => {
    if(message.content.startsWith("~") || message.content.startsWith("#")) {
        let command = message.content.slice(1);

        if((command.startsWith("play") || command.startsWith("queue")) && command.includes("spotify")) {
            message.channel.send("Converting...");
            let url = command.split(' ')[1];
            const data = {
                url: url
            };
            makePostRequest("http://spotitube_manager", data).then(res => {
                let result = 'Ready. To play, run command `~play ' + res.data + '`'
                message.channel.send(result);
            })
            .catch(_ => {
                message.channel.send("Failed :(");
            });
        }
        else if(command.startsWith("clearcache")) {
            makeRequest("http://spotitube_manager/clear").then(_ => {
                message.channel.send("Playlist cache cleared!");
            })
            .catch(_ => {
                message.channel.send("Failed :(");
            });
        }
    }
});

client.login(process.env.TOKEN);
