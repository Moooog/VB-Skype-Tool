using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Moogs_Skype_Tool
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            axSkype1.Attach();
            userLabel.Text = "Welcome: " + axSkype1.CurrentUserProfile.FullName;
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            nameBox.Text = axSkype1.CurrentUserProfile.FullName;
            moodBox.Text = axSkype1.CurrentUserProfile.RichMoodText;
            cityBox.Text = axSkype1.CurrentUserProfile.City;
            birthdayBox.Text = axSkype1.CurrentUserProfile.Birthday;
            aboutBox.Text = axSkype1.CurrentUserProfile.About;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = nameBox.Text;
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (!blinkingMoodCheckbox.Checked)
            {
                axSkype1.CurrentUserProfile.RichMoodText = moodBox.Text;
            }
            else if (blinkingMoodCheckbox.Checked)
            {
                axSkype1.CurrentUserProfile.RichMoodText = moodBox.Text;
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.City = cityBox.Text;
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.Birthday = birthdayBox.Text;
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.About = aboutBox.Text;
        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            timer1.Start();
            metroButton13.Enabled = true;
            metroButton11.Enabled = false;
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            metroButton11.Enabled = true;
            metroButton13.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusOnline;
            if (axSkype1.CurrentUserStatus == SKYPE4COMLib.TUserStatus.cusOnline)
            {
                axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusAway;
            }
            if (axSkype1.CurrentUserStatus == SKYPE4COMLib.TUserStatus.cusAway)
            {
                axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusDoNotDisturb;
            }
            if (axSkype1.CurrentUserStatus == SKYPE4COMLib.TUserStatus.cusDoNotDisturb)
            {
                axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusInvisible;
            }
            if (axSkype1.CurrentUserStatus == SKYPE4COMLib.TUserStatus.cusInvisible)
            {
                axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusOnline;
            }
            Invalidate();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusOnline;
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusAway;
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusDoNotDisturb;
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusInvisible;
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserStatus = SKYPE4COMLib.TUserStatus.cusOffline;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MetroFramework.MetroMessageBox.Show(Owner, "Do You Really Want To Exit?", "Moog's Private Skype Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void metroButton16_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MetroFramework.MetroMessageBox.Show(Owner, "Do You Really Want To Send Mass Message? This Can Take Alot Of Internet!", "Moog's Private Skype Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes && massMsgBox.Text != "")
            {
                if (!sendWithoutSentFromSkypeToolCheckbox.Checked)
                {
                    foreach (SKYPE4COMLib.User user in axSkype1.Friends)
                    {
                        axSkype1.SendMessage(user.Handle, massMsgBox.Text + "\n\n\n----Sent from Moog's private Skype tool!----");
                    }
                }
                else if (sendWithoutSentFromSkypeToolCheckbox.Checked)
                {
                    foreach (SKYPE4COMLib.User user in axSkype1.Friends)
                    {
                        axSkype1.SendMessage(user.Handle, massMsgBox.Text);
                    }
                }
            }
            if (dialog == DialogResult.No)
            {

            }
            if (massMsgBox.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(Owner, "Please Fill In The Field.", "Moog's Private Skype Tool");
            }
        }

        private void metroButton17_Click(object sender, EventArgs e)

        {
            if (usernameSpamBox.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Seems like you forgot to type in message or users name!", "Moog's Private Skype Tool");
            }
            if (spamUserBox.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Seems like you forgot to type in message or users name!", "Moog's Private Skype Tool");
            }
            timer2.Start();
            metroButton17.Enabled = false;
            metroButton18.Enabled = true;
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            metroButton18.Enabled = false;
            metroButton17.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (usernameSpamBox.Text != "" && spamUserBox.Text != "" && !sendWithTellingSentFromMoogsSkypeTool.Checked)
            {
                axSkype1.SendMessage(usernameSpamBox.Text, spamUserBox.Text);
            }
            else if (usernameSpamBox.Text != "" && spamUserBox.Text != "" && sendWithTellingSentFromMoogsSkypeTool.Checked)
            {
                axSkype1.SendMessage(usernameSpamBox.Text, spamUserBox.Text + "\n\n\n----Sent with Moog's Private Skype Tool----");
            }
        }

        private void metroButton16_Click_1(object sender, EventArgs e)
        {
            massMsgBox.Clear();
        }

        private void metroButton19_Click(object sender, EventArgs e)
        {
            spamUserBox.Clear();
        }

        private void metroButton21_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Moog_ <3";
        }

        private void metroButton20_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Arvens_";
        }

        private void metroButton22_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "MienKraft";
        }

        private void metroButton23_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Ez";
        }

        private void metroButton24_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Sooony";
        }

        private void metroButton25_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ROBLOXian";
        }

        private void metroButton26_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Skids";
        }

        private void metroButton27_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Sir Scrub";
        }

        private void metroButton28_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "MemersMeme";
        }

        private void metroButton29_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Much luv <3";
        }

        private void metroButton30_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Potatoe";
        }

        private void metroButton31_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Rat.exe";
        }

        private void metroButton32_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Deleted";
        }

        private void metroButton33_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "1337 Hax0r";
        }

        private void metroButton34_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "FatL";
        }

        private void metroButton35_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "420 Blaze m8";
        }

        private void metroButton36_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Daddy";
        }

        private void metroButton37_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "Cod Fanboy";
        }

        private void metroButton38_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "( ͡° ͜ʖ ͡°)";
        }

        private void metroButton39_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "¯\n_(ツ)_/¯";
        }

        private void metroButton40_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "̿̿ ̿̿ ̿̿ ̿'̿'\\n= ( ▀ ͜͞ʖ▀) =ε/̵͇̿̿/’̿’̿ ̿ ̿̿ ̿̿ ̿̿";
        }

        private void metroButton41_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "▄︻̷̿┻̿═━一";
        }

        private void metroButton42_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "( ͡°( ͡° ͜ʖ( ͡° ͜ʖ ͡°)ʖ ͡°) ͡°)";
        }

        private void metroButton43_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ʕ•ᴥ•ʔ";
        }

        private void metroButton44_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(▀̿Ĺ̯▀̿ ̿)";
        }

        private void metroButton45_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "̿(ง ͠° ͟ل͜ ͡°)ง";
        }

        private void metroButton46_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "༼ つ ◕_◕ ༽つ";
        }

        private void metroButton47_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ಠ_ಠ";
        }

        private void metroButton48_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(づ｡◕‿‿◕｡)づ";
        }

        private void metroButton49_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "̿'̿'\\n=( ͠° ͟ʖ ͡°)=ε/̵͇̿̿/'̿̿ ̿ ̿ ̿ ̿ ̿";
        }

        private void metroButton50_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ﾉ◕ヮ◕)ﾉ*:･ﾟ✧ ✧ﾟ･: *ヽ(◕ヮ◕ヽ)";
        }

        private void metroButton51_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "[̲̅$̲̅(̲̅5̲̅)̲̅$̲̅]";
        }

        private void metroButton52_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "┬┴┬┴┤ ͜ʖ ͡°) ├┬┴┬┴";
        }

        private void metroButton53_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "( ͡°╭͜ʖ╮͡° )";
        }

        private void metroButton54_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(͡ ͡° ͜ つ ͡͡°)";
        }

        private void metroButton55_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(• ε •)";
        }

        private void metroButton56_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ง'̀-'́)ง back away!";
        }

        private void metroButton57_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ಥ﹏ಥ)";
        }

        private void metroButton58_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "﴾͡๏̯͡๏﴿ O'RLY?";
        }

        private void metroButton59_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ノಠ益ಠ)ノ彡┻━┻";
        }

        private void metroButton60_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "[̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]";
        }

        private void metroButton61_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ﾉ◕ヮ◕)ﾉ*:･ﾟ✧";
        }

        private void metroButton62_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(☞ﾟ∀ﾟ)☞ Ye boiiii";
        }

        private void metroButton63_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "| (• ◡•)| (❍ᴥ❍ʋ)";
        }

        private void metroButton64_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(◕‿◕✿)";
        }

        private void metroButton65_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(ᵔᴥᵔ)";
        }

        private void metroButton66_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(╯°□°)╯︵ ʞooqǝɔɐɟ";
        }

        private void metroButton67_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(¬‿¬)";
        }

        private void metroButton68_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(☞ﾟヮﾟ)☞ ☜(ﾟヮﾟ☜)";
        }

        private void metroButton69_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(づ￣ ³￣)づ";
        }

        private void metroButton70_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ლ(ಠ益ಠლ) dafuq is wrong with you";
        }

        private void metroButton71_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ಠ╭╮ಠ";
        }

        private void metroButton72_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "̿ ̿ ̿'̿'\\n=(•_•)=ε/̵͇̿̿/'̿'̿ ̿";
        }

        private void metroButton73_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "/╲/\n╭( ͡° ͡° ͜ʖ ͡° ͡°)╮/\n╱\n";
        }

        private void metroButton74_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(;´༎ຶД༎ຶ`)";
        }

        private void metroButton75_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "♪~ ᕕ(ᐛ)ᕗ";
        }

        private void metroButton76_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "♥‿♥";
        }

        private void metroButton77_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "༼ つ  ͡° ͜ʖ ͡° ༽つ";
        }

        private void metroButton78_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "༼ つ ಥ_ಥ ༽つ";
        }

        private void metroButton79_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(╯°□°）╯︵ ┻━┻";
        }

        private void metroButton80_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "( ͡ᵔ ͜ʖ ͡ᵔ )";
        }

        private void metroButton81_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ヾ(⌐■_■)ノ♪";
        }

        private void metroButton82_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "~(˘▾˘~)";
        }

        private void metroButton83_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "◉_◉";
        }

        private void metroButton84_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "\n (•◡•) /";
        }

        private void metroButton85_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(~˘▾˘)~";
        }

        private void metroButton86_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "(._.) ( l: ) ( .-. ) ( :l ) (._.)";
        }

        private void metroButton87_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "༼ʘ̚ل͜ʘ̚༽";
        }

        private void metroButton88_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "༼ ºل͟º ༼ ºل͟º ༼ ºل͟º ༽ ºل͟º ༽ ºل͟º ༽";
        }

        private void metroButton89_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "┬┴┬┴┤(･_├┬┴┬┴";
        }

        private void metroButton90_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ᕙ(⇀‸↼‶)ᕗ";
        }

        private void metroButton91_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = "ᕦ(ò_óˇ)ᕤ";
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        private void metroButton92_Click(object sender, EventArgs e)
        {

            Clipboard.SetText(" ");

            DateTime dateTime;

            if (!DateTime.TryParse(quoteTimeText.Text, out dateTime))
                return;

            string user = userQuoteBox.Text;
            string message = quoteMsgBox.Text;
            string skypeMessageFragment = new XDocument(
                new XElement("quote",
                    new XAttribute("author", user),
                    new XAttribute("authorname", user),
                    new XAttribute("timestamp", (dateTime.ToUniversalTime() - epoch).TotalSeconds),
                    message)).ToString();

            IDataObject dataObject = new DataObject();
            dataObject.SetData("System.String", message);
            dataObject.SetData("Text", message);
            dataObject.SetData("UnicodeText", message);
            dataObject.SetData("OEMText", message);

            dataObject.SetData("SkypeMessageFragment",
                new MemoryStream(Encoding.UTF8.GetBytes(skypeMessageFragment)));

            dataObject.SetData("Locale",
                new MemoryStream(BitConverter.GetBytes(CultureInfo.CurrentCulture.LCID)));

            Clipboard.SetDataObject(dataObject);
        }

        private void metroButton14_Click_1(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.RichMoodText = "<blink>" + moodBox.Text + "</blink>";
        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("_" + italicTextBox.Text + "_");
        }

        private void metroButton95_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("*" + boldTextBox.Text + "*");
        }

        private void metroButton97_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("~" + crossedTextBox.Text + "~");
        }

        private void metroButton93_Click(object sender, EventArgs e)
        {
            italicTextBox.Clear();
        }

        private void metroButton94_Click(object sender, EventArgs e)
        {
            boldTextBox.Clear();
        }

        private void metroButton96_Click(object sender, EventArgs e)
        {
            crossedTextBox.Clear();
        }

        private void metroButton100_Click(object sender, EventArgs e)
        {
            MessageBox.Show(customPCMessageBox.Text, titlePCMessageBox.Text);
        }

        private void metroButton98_Click(object sender, EventArgs e)
        {
            axSkype1.ClearVoicemailHistory();
        }

        private void metroButton99_Click(object sender, EventArgs e)
        {
            axSkype1.ClearChatHistory();
        }

        private void metroButton101_Click(object sender, EventArgs e)
        {
            axSkype1.ClearCallHistory();
        }

        private void metroButton102_Click(object sender, EventArgs e)
        {
            axSkype1.ClearVoicemailHistory();
            axSkype1.ClearCallHistory();
            axSkype1.ClearCallHistory();
        }

        private void metroButton103_Click(object sender, EventArgs e)
        {
            axSkype1.Client.OpenAddContactDialog("echo123");
        }

        private void metroButton104_Click(object sender, EventArgs e)
        {
            axSkype1.Client.OpenProfileDialog();
        }

        private void metroButton105_Click(object sender, EventArgs e)
        {
            axSkype1.Client.OpenContactsTab();
        }

        private void metroButton106_Click(object sender, EventArgs e)
        {
            axSkype1.Client.OpenBlockedUsersDialog();
        }

        private void metroButton107_Click(object sender, EventArgs e)
        {
            axSkype1.Client.Minimize();
        }

        private void metroButton108_Click(object sender, EventArgs e)
        {
            axSkype1.Client.Shutdown();
        }

        private void metroButton14_Click(object sender, EventArgs e)
        {
            timer3.Start();
            realTimeNameChanger.Enabled = true;
            metroButton109.Enabled = true;
            metroButton14.Enabled = false;
        }

        private void metroButton109_Click(object sender, EventArgs e)
        {
            timer3.Stop();
            realTimeNameChanger.Enabled = false;
            metroButton14.Enabled = true;
            metroButton109.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.FullName = realTimeNameChanger.Text;
        }

        private void metroButton110_Click(object sender, EventArgs e)
        {
            timer4.Start();
            realMoodTimeChanger.Enabled = true;
            metroButton111.Enabled = true;
            metroButton110.Enabled = false;
        }

        private void metroButton111_Click(object sender, EventArgs e)
        {
            timer4.Stop();
            realMoodTimeChanger.Enabled = false;
            metroButton110.Enabled = true;
            metroButton111.Enabled = false;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.RichMoodText = realMoodTimeChanger.Text;
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.discord.gg/aXYrEVY");
        }

        private void metroLink4_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.discord.gg/aXYrEVY");
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {

        }

        private void metroLink3_Click(object sender, EventArgs e)
        {

        }

        private void metroButton112_Click(object sender, EventArgs e)
        {
            mobileNumberBox.Text = axSkype1.CurrentUserProfile.PhoneMobile;
            homeMobileNumberBox.Text = axSkype1.CurrentUserProfile.PhoneHome;
            workNumberBox.Text = axSkype1.CurrentUserProfile.PhoneOffice;
        }

        private void metroButton113_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.PhoneMobile = mobileNumberBox.Text;
        }

        private void metroButton114_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.PhoneHome = homeMobileNumberBox.Text;
        }

        private void metroButton115_Click(object sender, EventArgs e)
        {
            axSkype1.CurrentUserProfile.PhoneOffice = workNumberBox.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroButton116_Click(object sender, EventArgs e)
        {
  
                        }
                    }
                }
