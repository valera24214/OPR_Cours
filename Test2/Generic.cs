using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace Test2
{
    public class Generic
    {
        List<double> fitneces;

        private int count_of_generations;

        private int population_length;

        private int up_gen;

        private int low_gen;

        private int up_bound;

        private int low_bound;

        private double mutation_Rate;

        private double[,] coefs;

        private List<List<Individ>> populations;

        public Generic(int count_of_generations, int population_length, int up_gen, int low_gen, int up_bound, int low_bound, double mutation_Rate, double[,] coefs)
        {
            fitneces = new List<double>();

            this.count_of_generations = count_of_generations;
            this.population_length = population_length;
            this.up_gen = up_gen;
            this.low_gen = low_gen;
            this.up_bound = up_bound;
            this.low_bound = low_bound;
            this.mutation_Rate = mutation_Rate;
            this.coefs = coefs;

            populations = new List<List<Individ>>();
        }

        private bool Satisf_limits(Individ individ)
        {
            bool disicion = true;

            double x = individ.x;
            double y = individ.y;
            double z = individ.z;

            for (int i = 0; i < coefs.GetLength(0) - 1; i++)
            {
                if (x * coefs[i, 1] + y * coefs[i, 2] + z * coefs[i, 3] > coefs[i, 0])
                {
                    disicion = false;
                }    
            }
            
            return disicion;
        }

        private List<Individ> Selection(List<Individ> individs)
        {
            individs = individs.OrderByDescending(i => i.Fitness_func()).ToList();
            int count = individs.Count;
            List<Individ> to_delete = new List<Individ>();
            for (int i = 0; i < count; i++)
            {
                if (!Satisf_limits(individs[i]))
                {
                    to_delete.Add(individs[i]);
                }
            }

            foreach (var i in to_delete)
            {
                individs.Remove(i);
            }

            return individs;
        }

        private List<Individ> Crossover(List<Individ> parents, int num_offsprings)
        {
            List<Individ> offsprings = new List<Individ>();
            Random rnd = new Random();
            for (int i = 0; i < num_offsprings; i++)
            {
                Individ parent1 = parents[rnd.Next(parents.Count)];
                Individ parent2 = parents[rnd.Next(parents.Count)];

                int gen = rnd.Next() * (2-1)+1;

                Individ offspring1;
                Individ offspring2;

                if (gen == 1)
                {
                    offspring1 = new Individ()
                    {
                        x = parent1.x,
                        y = parent2.y,
                        z = parent2.z,

                        coefs = new double[]
                        {
                            -this.coefs[4,1],
                            -this.coefs[4,2],
                            -this.coefs[4,3],
                        }

                    };

                    offspring2 = new Individ()
                    {
                        x = parent2.x,
                        y = parent1.y,
                        z = parent1.z,

                        coefs = new double[]
                        {
                            -this.coefs[4,1],
                            -this.coefs[4,2],
                            -this.coefs[4,3],
                        }

                    };
                }

                else
                {
                    offspring1 = new Individ()
                    {
                        x = parent1.x,
                        y = parent1.y,
                        z = parent2.z,

                        coefs = new double[]
                        {
                            -this.coefs[4,1],
                            -this.coefs[4,2],
                            -this.coefs[4,3],
                        }
                    };

                    offspring2 = new Individ()
                    {
                        x = parent2.x,
                        y = parent2.y,
                        z = parent1.z,

                        coefs = new double[]
                        {
                            -this.coefs[4,1],
                            -this.coefs[4,2],
                            -this.coefs[4,3],
                        }
                    };
                }
                

                offsprings.Add(offspring1);
                offsprings.Add(offspring2);
            }

            return offsprings;
        }

        private List<Individ> Mutation(List<Individ> offsprings)
        {
            Random rnd = new Random();
            offsprings = offsprings.OrderByDescending(o => o.Fitness_func()).ToList();

            foreach (var off in offsprings)
            {
                if (rnd.NextDouble() < mutation_Rate)
                {
                    int mut_type = rnd.Next(10);

                    switch (mut_type)
                    {
                        case 0:
                        case 10:
                            {
                                off.x += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 1:
                            {
                                off.x -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 2:
                            {
                                off.x += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 3:
                            {
                                off.x += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 4:
                            {
                                off.x -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 5:
                            {
                                off.x += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 6:
                            {
                                off.x -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y += Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 7:
                            {
                                off.x -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.y -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                off.z -= Math.Round(rnd.NextDouble() * (up_bound - low_bound) + low_bound);
                                break;
                            }

                        case 8:
                        case 9:
                            {
                                off.x = Math.Round(rnd.NextDouble() * (up_gen - low_gen) + low_gen);
                                off.y = Math.Round(rnd.NextDouble() * (up_gen - low_gen) + low_gen);
                                off.z = Math.Round(rnd.NextDouble() * (up_gen - low_gen) + low_gen);
                                break;
                            }

                    }
                }
            }


            return offsprings;
        }

        public double[] Count()
        {
            double[] result = new double[3];
            List<Individ> population = new List<Individ>();
            Random random = new Random();
            for (int i = 0; i < population_length; i++)
            {
                Individ individ = new Individ()
                {
                    x = Math.Round(random.NextDouble() * (up_gen - low_gen) + low_gen),
                    y = Math.Round(random.NextDouble() * (up_gen - low_gen) + low_gen),
                    z = Math.Round(random.NextDouble() * (up_gen - low_gen) + low_gen),

                    coefs = new double[] 
                    {
                        -this.coefs[4,1],
                        -this.coefs[4,2],
                        -this.coefs[4,3],
                    } 
                };

                population.Add(individ);
            }

            populations.Add(population);

            for (int i = 0; i < count_of_generations; i++)
            {
                population = Selection(population);
                fitneces.Add(Math.Round(population[0].Fitness_func(), 2));
                int count = population.ToArray().Length;
                if (count > population_length / 2)
                {
                    while (population.Count != population_length / 2)
                    {
                        population.RemoveAt(population.Count - 1);
                    }
                }
                List<Individ> offsprings = Crossover(population, population_length/2);
                offsprings = Mutation(offsprings);
                population.AddRange(Selection(offsprings));
                populations.Add(population);
            }

            result[0] = Math.Round(population[0].x, 2);
            result[1] = Math.Round(population[0].y, 2);
            result[2] = Math.Round(population[0].z, 2);

            return result;
        }


        public double[] Return_fit()
        {
            using (StreamWriter writter = new StreamWriter("E:\\Lab\\4 курс\\ОПР\\Курсач\\populations.txt", false))
            {
                foreach (var population in populations)
                {
                    foreach (var individ in population)
                    {
                        writter.WriteLine(individ.x + ", " + individ.y + ", " + individ.z + "; " + individ.Fitness_func());
                    }

                    writter.WriteLine(" ");
                }
            }
            return fitneces.ToArray();
        }
    }
}
