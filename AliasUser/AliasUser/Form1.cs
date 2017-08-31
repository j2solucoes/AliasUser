using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliasUser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LerDados();
        }

        private bool HeuristicBird(string nameCompleteA, string nameCompleteB, string emailCompleteA, string emailCompleteB)
        {
            bool simil = false;
            decimal threshold = 93;

            if (nameCompleteA != null && nameCompleteB != null)
            {
                //simil(completeNameA, completeNameB) ≥ t;
                if (CalcLevenshtein(nameCompleteA, nameCompleteB) >= threshold)
                {
                    simil = true;
                }
                else
                {
                    string[] namesA = nameCompleteA.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    string[] namesB = nameCompleteB.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (namesA.Length > 0 && namesB.Length > 0)
                    {
                        //simil(firstNameA, firstNameB) ≥ t and simil(lastNameA, lastNameB) ≥ t;
                        if (CalcLevenshtein(namesA[0], namesB[0]) >= threshold && CalcLevenshtein(namesA[namesA.Length - 1], namesB[namesB.Length - 1]) >= threshold)
                        {
                            simil = true;
                        }
                        else
                        {
                            if (emailCompleteA != null && emailCompleteB != null)
                            {
                                string[] emailsA = emailCompleteA.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                                string[] emailsB = emailCompleteB.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);

                                if (emailsB.Length > 0 && emailsA.Length > 0)
                                {
                                    //prefixB contains firstNameA and lastNameA;
                                    if (emailsB[0].Contains(namesA[0]) && emailsB.Contains(namesA[namesA.Length - 1]))
                                    {
                                        simil = true;
                                    }
                                    //prefixB contains firstNameA and the initial of lastNameA;
                                    else if (emailsB.Contains(namesA[0]) && emailsB.Contains(namesA[namesA.Length - 1][0].ToString()))
                                    {
                                        simil = true;
                                    }
                                    //prefixB contains the initial of firstNameA and the lastNameA;
                                    else if (emailsB.Contains(namesA[namesA.Length - 1]) && emailsB.Contains(namesA[0][0].ToString()))
                                    {
                                        simil = true;
                                    }
                                    //simil(prefixA, prefixB) ≥ t.
                                    else if (!emailsA[0].Trim().ToUpper().Equals("GITHUB") && CalcLevenshtein(namesA[0], namesB[0]) >= threshold / 2 && CalcLevenshtein(emailsA[0], emailsB[0]) >= threshold)
                                    {
                                        simil = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return simil;
        }

        private decimal CalcLevenshtein(string a, string b)
        {
            decimal result = ((a.Length - Levenshtein(a, b)) * 100) / a.Length;

            return result;
        }

        private async void LerDados()
        {
            HashSet<string> logins = new HashSet<string>();

            try
            {

                ds1.Clear();

                label4.Text = "Searching repository";
                var ghe = new Uri(textBox1.Text);
                var client = new GitHubClient(new ProductHeaderValue("AliasUserGitHub"), ghe);


                label4.Text = "Performing user login";
                var basicAuth = new Credentials(textBox2.Text, maskedTextBox1.Text); // NOTE: not real credentials
                client.Credentials = basicAuth;

                string[] partes = textBox1.Text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length >= 2)
                {
                    Repository r = await client.Repository.Get(partes[partes.Length - 2], partes[partes.Length - 1].Replace(".git", ""));


                    label4.Text = "Recovering contributors";
                    IReadOnlyList<RepositoryContributor> repositorios = await client.Repository.GetAllContributors(r.Id);

                    Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

                    List<User> users = new List<User>();


                    foreach (RepositoryContributor item in repositorios)
                    {
                        label4.Text = item.Login != null ? "Recovering User " + item.Login : "";
                        User user = await client.User.Get(item.Login);
                        users.Add(user);
                    }


                    label4.Text = "Processing Alias";
                    foreach (User user in users)
                    {

                        ds1.User.AddUserRow(user.Name != null ? user.Name : string.Empty,
                                            user.Email != null ? user.Email : string.Empty,
                                            user.Login);

                        foreach (User userAlis in users)
                        {
                            if (!user.Login.Equals(userAlis.Login))
                            {
                                if (!logins.Contains(userAlis.Login))
                                {
                                    if (HeuristicBird(user.Name, userAlis.Name, user.Email, userAlis.Email))
                                    {
                                        Ds.AliasRow newRow = null;


                                        if (!logins.Contains(user.Login))
                                        {
                                            newRow = ds1.Alias.NewAliasRow();
                                            ds1.Alias.AddAliasRow(newRow);

                                            logins.Add(user.Login);

                                            Ds.LoginsRow newRowLogin = ds1.Logins.NewLoginsRow();
                                            newRowLogin.AliasRow = newRow;
                                            newRowLogin.Email = user.Email != null ? user.Email : string.Empty;
                                            newRowLogin.Login = user.Login;
                                            newRowLogin.Nome = user.Name;
                                            ds1.Logins.AddLoginsRow(newRowLogin);
                                        }

                                        if (!logins.Contains(userAlis.Login))
                                        {
                                            if (newRow == null)
                                            {
                                                newRow = ds1.Alias.Where(w => w.ItemAlias == ds1.Logins.Where(y => y.Login.Equals(user.Login)).First().ItemAlias).First();
                                            }

                                            logins.Add(userAlis.Login);

                                            Ds.LoginsRow newRowLogin = ds1.Logins.NewLoginsRow();
                                            newRowLogin.AliasRow = newRow;
                                            newRowLogin.Email = userAlis.Email != null ? userAlis.Email : string.Empty;
                                            newRowLogin.Login = userAlis.Login;
                                            newRowLogin.Nome = userAlis.Name;
                                            ds1.Logins.AddLoginsRow(newRowLogin);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Invalid url.");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, "Error loading Repository.");
            }
            finally
            {
                label4.Text = "Waiting...";
            }
        }

        private Int32 Levenshtein(String a, String b)
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }
    }
}