using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{

    private Dictionary<int, Competition> competitions;
    private Dictionary<int, Competitor> competitors;

    public Olympics()
    {
        this.competitions = new Dictionary<int, Competition>();

        this.competitors = new Dictionary<int, Competitor>();
    }


    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var newCompetition = new Competition(name, id, participantsLimit);

        this.competitions.Add(id, newCompetition);

    }

    public void AddCompetitor(int id, string name)
    {
        if (this.competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var competitor = new Competitor(id, name);

        this.competitors.Add(id, competitor);
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }

        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var competitor = this.competitors[competitorId];
        var competition = this.competitions[competitionId];

        competitor.TotalScore = competition.Score;

        competition.Competitors.Add(competitor);
    }

    public int CompetitionsCount()
    {
        return this.competitions.Count();
    }

    public int CompetitorsCount()
    {
        return this.competitors.Count();
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var competition = this.competitions[competitionId];

        var competitor = competition.Competitors.FirstOrDefault(x => x.Id == comp.Id);

        if (competitor != null)
        {
            return true;
        }

        return false;

    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitors.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }

        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var competitor = this.competitors[competitorId];

        var competition = this.competitions[competitionId];
        
        if (competition.Competitors.Remove(competitor))
        {
            competitor.TotalScore -= competition.Score;
        }
        
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        var result = new List<Competitor>();

        foreach (var item in this.competitors)
        {
            if (item.Value.TotalScore > min && item.Value.TotalScore <= max)
            {
                result.Add(item.Value);
            }
        }

        return result.OrderBy(x => x.Id);
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        List<Competitor> result = new List<Competitor>();

        foreach (var item in this.competitors)
        {
            if (item.Value.Name == name)
            {
                result.Add(item.Value);
            }
        }

        if (result.Count == 0)
        {
            throw new ArgumentException();
        }

        return result.OrderBy(x => x.Id);
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.competitions[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        var result = new List<Competitor>();

        foreach (var item in this.competitors)
        {
            if (item.Value.Name.Length >= min && item.Value.Name.Length <= max)
            {
                result.Add(item.Value);
            }
        }

        return result.OrderBy(x => x.Id);
    }
}