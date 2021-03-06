﻿using System;
using System.Linq;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperIndividual : Individual
    {
        private static Random Random = new Random();

        public SkyscraperIndividual(VertexGraph graph)
        {
            Graph = graph;
        }

        public override int CalculateFitness()
        {
            return (int) SimulatedGraph.Vertices.OrderByDescending(a => a.Z).First().ScaledY;
        }
    }
}
