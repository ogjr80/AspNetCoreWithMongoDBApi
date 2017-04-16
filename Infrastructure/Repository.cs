using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NotesAppApi.Model;

namespace NotesAppApi.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly NoteContext _context = null; 

        public Repository(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings); 
        }
        public async Task AddNote(Note item)
        {
            try{
                    await _context.Notes.InsertOneAsync(item); 
        
            }
            catch(Exception ex){
                throw ex; 
            }    
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try{                    
                return await _context.Notes.Find(_ => true).ToListAsync(); 
            }
            catch (Exception ex){
             throw ex; 
            }
        }

        public async Task<Note> GetNote(string id)
        {
            try {
            var filter = Builders<Note>.Filter.Eq("Id", id);
            return await _context.Notes.Find(filter).FirstOrDefaultAsync(); 
        
            }
            catch (Exception ex){
                throw ex; 
            }
        }

        public async Task<DeleteResult> RemoveAllNotes()
        {
            try{
                    return await _context.Notes.DeleteManyAsync(new BsonDocument());
            }
            catch(Exception ex){
                throw ex; 
            }
        }

        public async Task<DeleteResult> RemoveNote(string id)
        {

            try{
                return await _context.Notes.DeleteOneAsync(Builders<Note>.Filter.Eq("Id", id));
            }
            catch(Exception ex){
                throw ex; 
            }
        }

        public async Task<UpdateResult> UpdateNote(string id, string body)
        {
            try{
                var filter = Builders<Note>.Filter.Eq( s => s.Id ,id);
                var update = Builders<Note>.Update
                                .Set(s => s.Body, body)
                                .CurrentDate(s => s.UpdatedOn);
                return await _context.Notes.UpdateOneAsync(filter, update);
        
            }
            catch(Exception ex){
                throw ex; 
            }    
        }

        public async Task<ReplaceOneResult> UpdateNote(string id, Note item)
        {
            try{
                 return await _context.Notes
                             .ReplaceOneAsync(n => n.Id.Equals(id)
                                                 , item
                                                 , new UpdateOptions { IsUpsert = true });
        
            }
            catch(Exception ex){
                throw ex; 
            }    
        }

       

        public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}