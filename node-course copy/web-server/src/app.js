const path = require('path')
const express = require('express')
const hbs = require ('hbs')
const geocode = require ('./utils/geocode')
const forecast = require ('./utils/forecast')

const app = express()

//Define path for Express config
const publicDirectoryPath = path.join(__dirname, '../public')
const viewsPath = path.join(__dirname, '../templates/views')
const partialsPath = path.join(__dirname, '../templates/partials')

//setup  static directory to serve
app.use(express.static(publicDirectoryPath))

//Setup handlabars engine and views location
app.set('view engine','hbs')
app.set('views', viewsPath)
hbs.registerPartials(partialsPath)

app.get ('',(req, res) => {
    res.render('index', {
        title: 'Weather',
        name: 'Robert A'
    })
})

app.get ('/about', (req,res) => {
    res.render('about', {
        title: 'About Page',
        name: 'Robert A'
    })
})

app.get('/weather', (req, res) => {

    if (!req.query.address) {
        return res.send ({
            error: 'Address is required'
        })
    }

    geocode(req.query.address, (error, { latitude, longitude, location } = {}) => {
        if (error) {
            return res.send({ error })
        }

        forecast(latitude, longitude, (error, forecastData) => {
            if (error) {
                return res.send({ error })
            }

            res.send({
                location,
                forecast: forecastData
            })
        })
    })
})

app.get ('/help', (req, res) => {
    res.render ('help',{
        title: 'Help Page',
        name: 'Robert A'
    })
})

app.get ('/help/*', (req,res)=>{
    res.render('404',{
        title: '404',
        name: 'Robert A',
        errormessage: 'Help article not found'

    })
})

app.get('/products', (req, res)=> {
    if (!req.query.search) {
        return res.send({
            error: 'You must provide a search term'
        })
    }

    console.log(req.query)
    res.send ({
        products: []
    })
})

app.get ('*', (req, res)=> {
    res.render ('404',{
        title: '404',
        name: 'Robert A',
        errormessage: 'Page not found'
    })
})



app.listen(3001, () => {
    console.log('Server is up on port 3001.')
})